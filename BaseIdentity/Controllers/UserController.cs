using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.BusinessLayer.Concrete;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Document = iTextSharp.text.Document;
using Image = iTextSharp.text.Image;
//using PuppeteerSharp;
//using PuppeteerSharp.Media;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class UserController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly IFriendshipService _friendshipManager;


        public UserController(UserManager<AppUser> userManager, IFriendshipService friendshipManager)
        {
            _userManager = userManager;
            _friendshipManager = friendshipManager;

        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> UserEditProfile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel();

            userUpdateViewModel.Name = values.Name;
            userUpdateViewModel.Surname = values.Surname;
            userUpdateViewModel.UserName = values.UserName;
            userUpdateViewModel.ImageUrl = values.Image;
            userUpdateViewModel.Email = values.Email;
            userUpdateViewModel.PhoneNumber = values.PhoneNumber;
            userUpdateViewModel.CurrentWork = values.CurrentWork;
            userUpdateViewModel.Linkedin = values.Linkedin;
            userUpdateViewModel.Skills = values.Skills;
            userUpdateViewModel.Explanation = values.Explanation;
            userUpdateViewModel.DateOfBirth = values.DateOfBirth;

            return View(userUpdateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UserEditProfile(UserUpdateViewModel model)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);


            if (model.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(model.Image.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/UserImage/" + imageName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await model.Image.CopyToAsync(stream);
                values.Image = imageName;
                //imageName;
            }

            values.Name = model.Name;
            values.Surname = model.Surname;
            values.UserName = model.UserName;
            values.Email = model.Email;
            values.PhoneNumber = model.PhoneNumber;
            values.CurrentWork = model.CurrentWork;
            values.Linkedin = model.Linkedin;
            values.Skills = model.Skills;
            values.Explanation = model.Explanation;
            values.DateOfBirth = model.DateOfBirth;

            var saved = await _userManager.UpdateAsync(values);
            if (saved.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }

            else
            {
                ModelState.AddModelError("", "An error occured.");
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(values);
        }



        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Settings", "User");
            }
        }



        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(UserUpdateViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (model.NewPassword == model.ConfirmNewPassword)
            {
                var updateUser = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (updateUser.Succeeded)
                {
                    // Get the current user's ID
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // Delete the user's cookies
                    await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

                    // Redirect the user to the home page
                    return RedirectToAction("SignIn", "Login");

                }
            }
            else
            {
                ModelState.AddModelError("", "An error occured.");
            }
            return RedirectToAction("User", "Settings");
        }






        [HttpGet]
        public async Task<IActionResult> Find(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userSigned = await _userManager.FindByNameAsync(User.Identity.Name);


            var friends = _friendshipManager.ListFriends(userSigned.Id).Where(x => x.FriendId == id && x.AppUserId == userSigned.Id);


            if (friends.Any())
            {
                ViewBag.follows = true;
            }


            return View(user);
        }



        [HttpGet]
        public async Task<IActionResult> List()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var id = user.Id;
            var users = await _userManager.Users.ToListAsync();
            ViewBag.currentUserId = id;
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> DownloadPdf(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PdfReports/" + "user.pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();


            // Add personal information
            Paragraph name = new Paragraph($"{user.Name + " " + user.Surname}");
            name.Alignment = Element.ALIGN_LEFT;
            name.Font = FontFactory.GetFont(FontFactory.HELVETICA, 24, Font.BOLD);
            document.Add(name);

            Paragraph address = new Paragraph($"{user.PhoneNumber}");
            address.Alignment = Element.ALIGN_LEFT;
            address.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
            document.Add(address);

            Paragraph phone = new Paragraph($"{user.Email}");
            phone.Alignment = Element.ALIGN_LEFT;
            phone.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
            document.Add(phone);

            Paragraph email = new Paragraph($"{user.Linkedin}");
            email.Alignment = Element.ALIGN_LEFT;
            email.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
            document.Add(email);

            document.Add(new Paragraph("\n")); //add empty line 

            // Add a section header
            Paragraph summaryHeader = new Paragraph("Summary");
            summaryHeader.Alignment = Element.ALIGN_LEFT;
            summaryHeader.Font.Color = BaseColor.BLUE;
            summaryHeader.Font = FontFactory.GetFont(FontFactory.HELVETICA, 18, Font.BOLD);
            document.Add(summaryHeader);

            // Add summary text
            Paragraph summary = new Paragraph($"{user.Explanation}");
            document.Add(summary);

            //Add  a photo
            string photoPath = "https://avatars.githubusercontent.com/u/107196935?v=4";
           Image photo = Image.GetInstance(photoPath);
            photo.ScaleToFit(100f, 150f);
            photo.SetAbsolutePosition(450f, 750f);
            document.Add(photo);

            document.Add(new Paragraph("\n")); //add empty line 

            Paragraph experienceHeader = new Paragraph("Experience");
            experienceHeader.Alignment = Element.ALIGN_LEFT;
            experienceHeader.Font.Color = BaseColor.BLUE;
            experienceHeader.Font = FontFactory.GetFont(FontFactory.HELVETICA, 18, Font.BOLD);
            document.Add(experienceHeader);

         
            // Add job experience
            Paragraph jobTitle = new Paragraph($"{user.CurrentWork}");
            jobTitle.Alignment = Element.ALIGN_LEFT;
            jobTitle.Font = FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD);
            document.Add(jobTitle);

            Paragraph jobTitle2 = new Paragraph($"{user.ExWork1}");
            jobTitle2.Alignment = Element.ALIGN_LEFT;
            jobTitle2.Font = FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD);
            document.Add(jobTitle2);

            Paragraph jobTitle3 = new Paragraph($"{user.ExWork2}");
            jobTitle3.Alignment = Element.ALIGN_LEFT;
            jobTitle3.Font = FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD);
            document.Add(jobTitle3);

            //Paragraph jobDates = new Paragraph("January 2020 - Present");
            //jobDates.Alignment = Element.ALIGN_LEFT;
            //jobDates.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
            //document.Add(jobDates);

            //Paragraph jobDescription = new Paragraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed auctor, magna a feugiat placerat, ipsum risus accumsan augue, et malesuada nibh orci id quam. Sed rutrum, ipsum at malesuada dictum, ipsum velit aliquam velit, id aliquam turpis nulla vel nisl.");
            //document.Add(jobDescription);

            document.Add(new Paragraph("\n")); //add empty line 

            // Add a section header
            Paragraph educationHeader = new Paragraph("Education");
            educationHeader.Alignment = Element.ALIGN_LEFT;
            educationHeader.Font.Color = BaseColor.BLUE;
            educationHeader.Font = FontFactory.GetFont(FontFactory.HELVETICA, 18, Font.BOLD);
            document.Add(educationHeader);

            // Add education
            Paragraph degree = new Paragraph($"{user.UniversityName + " - " + user.DepartmentofUniversity }");
            degree.Alignment = Element.ALIGN_LEFT;
            degree.Font = FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD);
            document.Add(degree);

            //Paragraph graduationDate = new Paragraph($"{user.}");
            //graduationDate.Alignment = Element.ALIGN_LEFT;
            //graduationDate.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
            //document.Add(graduationDate);

            // Add a section header
            Paragraph languageHeader = new Paragraph("Language");
            languageHeader.Alignment = Element.ALIGN_LEFT;
            languageHeader.Font.Color = BaseColor.BLUE;
            languageHeader.Font = FontFactory.GetFont(FontFactory.HELVETICA, 18, Font.BOLD);
            document.Add(languageHeader);

            // Add education
            Paragraph langs = new Paragraph($"{user.Languages}");
            langs.Alignment = Element.ALIGN_LEFT;
            langs.Font = FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD);
            document.Add(langs);


            document.Close();

            return File("/PdfReports/user.pdf", "application/pdf", "user.pdf");









            //            var browserFetcher = new BrowserFetcher();
            //            await browserFetcher.DownloadAsync();
            //            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            //            {
            //                Headless = true
            //            });
            //            await using var page = await browser.NewPageAsync();
            //            await page.EmulateMediaTypeAsync(MediaType.Screen);
            //            var head = "<head>   <meta charset=\"utf-8\">   <title>Invoice</title>     <style>   .invoice-box {       max-width: 800px;       margin: auto;       padding: 30px;       border: 1px solid #eee;       box-shadow: 0 0 10px rgba(0, 0, 0, .15);       font-size: 16px;       line-height: 24px;       font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;       color: #555;   }     .invoice-box table {       width: 100%;       line-height: inherit;       text-align: left;   }     .invoice-box table td {       padding: 5px;       vertical-align: top;   }     .invoice-box table tr td:nth-child(2) {       text-align: right;   }     .invoice-box table tr.top table td {       padding-bottom: 20px;   }     .invoice-box table tr.top table td.title {       font-size: 45px;       line-height: 45px;       color: #333;   }     .invoice-box table tr.information table td {       padding-bottom: 40px;   }     .invoice-box table tr.heading td {       background: #eee;       border-bottom: 1px solid #ddd;       font-weight: bold;   }     .invoice-box table tr.details td {       padding-bottom: 20px;   }     .invoice-box table tr.item td{       border-bottom: 1px solid #eee;   }     .invoice-box table tr.item.last td {       border-bottom: none;   }     .invoice-box table tr.total td:nth-child(2) {       border-top: 2px solid #eee;       font-weight: bold;   }   </style></head>";

            //            var body = $@"<body>
            //   <div class=""invoice-box"">
            //       <table cellpadding=""0"" cellspacing=""0"">
            //           <tr class=""top"">
            //               <td colspan=""2"">
            //                   <table>
            //                       <tr>
            //                           <td class=""title"">
            //                               <img src=""https://localhost:5001/UserImage/{(user.Image != null ? user.Image : "")}"" style=""width:100%; max-width:150px;"">
            //                           </td>
            //                          <td>
            //                        <b><h1>{(user.Name != null ? user.Name : "")} {(user.Surname != null ? user.Surname : "")}</h1></b>
            //                        <h2>{(user.Title != null ? user.Title : "")}</b>
            //                        </td>

            //                       </tr>
            //                   </table>
            //               </td>
            //           </tr>

            //           <tr class=""information"">
            //               <td colspan=""2"">
            //                   <table>
            //                       <tr>
            //                           <td>
            //                               <b>Date of Birth: </b>{(user.DateOfBirth != null ?  user.DateOfBirth.ToShortDateString() : "")}<br>
            //                               <b>University: </b>{(user.UniversityName != null ? user.UniversityName : "")} - {(user.DepartmentofUniversity != null ? user.DepartmentofUniversity : "")}<br>
            //                                <b>Mail: </b> {user.Email}<br>

            //                           </td>
            //                           <td>
            //                               <b>Driver License: </b> {(user.DriverLicense != null ? "Yes" : "No")}<br>
            //                               <b>Phone: </b> {(user.PhoneNumber != null ? user.PhoneNumber : "")}<br>
            //                                <b>Linkedin: </b> {(user.Linkedin != null ? user.Linkedin : "")}<br>
            //                           </td>
            //                       </tr>
            //                   </table>
            //               </td>
            //           </tr>

            //           <tr class=""heading"">
            //               <td>
            //                  About
            //               </td>

            //               <td>

            //               </td>
            //           </tr>

            //           <tr class=""details"">
            //               <td>
            //                  {(user.Explanation != null ? user.Explanation : "")}
            //               </td>
            //           </tr>

            //            <tr class=""heading"">
            //               <td>
            //                  Skills
            //               </td>

            //               <td>

            //               </td>
            //           </tr>

            //           <tr class=""details"">
            //               <td>
            //                  {(user.Skills != null ? user.Skills : "")}
            //               </td>
            //           </tr>

            //  <tr class=""heading"">
            //               <td>
            //                  Experience
            //               </td>

            //               <td>

            //               </td>
            //           </tr>

            //           <tr class=""details"">
            //                <td>
            //                {(user.CurrentWork != null ? user.CurrentWork : "")} ,
            //   {(user.ExWork1 != null ? user.ExWork1 : "")} ,

            //    {(user.ExWork2 != null ? user.ExWork2 : "")}
            //               </td>

            //               <td>

            //               </td>


            //               <td>

            //               </td>
            //           </tr>
            // <tr class=""heading"">
            //               <td>
            //                  Courses
            //               </td>

            //               <td>

            //               </td>
            //           </tr>

            //           <tr class=""details"">
            //               <td>
            //                  {(user.Courses != null ? user.Courses : "")}
            //               </td>
            //           </tr>

            // <tr class=""heading"">
            //               <td>
            //                  Languages
            //               </td>

            //               <td>

            //               </td>
            //           </tr>

            //           <tr class=""details"">
            //               <td>
            //                  {(user.Languages != null ? user.Languages : "")}
            //               </td>
            //           </tr>

            //           </tr>
            //       </table>
            //   </div>
            //</body>";

            //            await page.SetContentAsync(@"<!doctype html> <html>" + head + body + "</html>");
            //            var pdfContent = await page.PdfStreamAsync(new PdfOptions
            //            {
            //                Format = PaperFormat.A4,
            //                PrintBackground = true
            //            });
            //            return File(pdfContent, "application/pdf", $@"{user.Name + "_" + user.Surname}.pdf");
            //        }


        }
    }
}
