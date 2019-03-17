using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreWebsiteTest1.OtherClasses;
using dbHandle = CoreWebsiteTest1.OtherClasses.MyDBController;

namespace CoreWebsiteTest1.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
