using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Parse;

namespace uMAD.Data
{
    [Parse.ParseClassName("UMAD")]
    public class UMAD : Parse.ParseObject
    {
        [Parse.ParseFieldName("year")]
        public int Year
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }

        public static async Task<UMAD> GetCurrentUMad()
        {
            var currentYear = DateTime.Now.Year;
            var query = from item in new Parse.ParseQuery<UMAD>() where item.Year == currentYear select item;
            return await query.FirstAsync();
        }

    }
}