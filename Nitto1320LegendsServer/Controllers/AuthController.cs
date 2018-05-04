using System.Security.Claims;
using System.Threading.Tasks;
using Nitto1320LegendsServer.Auth;
using Nitto1320LegendsServer.Models;
using Nitto1320LegendsServer.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Nitto1320LegendsServer.Classes;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace Nitto1320LegendsServer.Controllers
{
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(username, password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            //JwtToken jwt = await Tokens.GenerateJwt(identity, _jwtFactory, username, _jwtOptions, new JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.None });
            //List<Claim> claims = identity.Claims.ToList();
            //AppUser user = await _userManager.GetUserAsync(new ClaimsPrincipal(identity));
            AppUser user = await GetUserAsync(username, password);
            LoginXml loginXml = new LoginXml
            {
                bg = "#CFFFFF",
                dc = 1,
                i = "1",// user.Id,
                im = "2",
                lid = 300,
                m = 666,
                p = 777,
                sc = 100001,
                ti = 1,
                tr = 1,
                u = user.UserName
            };

            XmlDocument xdoc = new XmlDocument();
            XmlElement root = (XmlElement)xdoc.AppendChild(xdoc.CreateElement("n2"));
            XmlElement loginXmlNode = (XmlElement)root.AppendChild(xdoc.ImportNode(SerializeToXmlElement(loginXml), false));
            
            Response.StatusCode = 200;
            return Content(XmlToString(xdoc), "application/xml");
        }
        
        public static XmlElement SerializeToXmlElement(object o)
        {
            XmlDocument doc = new XmlDocument();

            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(o.GetType()).Serialize(writer, o);
            }

            doc.DocumentElement.Attributes.RemoveNamedItem("xmlns:xsi");
            doc.DocumentElement.Attributes.RemoveNamedItem("xmlns:xsd");
            
            return doc.DocumentElement;
        }

        public static string XmlToString(XmlNode xml)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                xml.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        private async Task<AppUser> GetUserAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            AppUser user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return null;

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }

            return null;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}