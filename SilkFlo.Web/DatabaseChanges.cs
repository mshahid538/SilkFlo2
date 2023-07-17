using System;
using System.Threading.Tasks;
using SilkFlo.Data.Core;

namespace SilkFlo.Web
{
    public class DatabaseChanges
    {
        public static async Task Fixes(IUnitOfWork unitOfWork)
        {
            //await Fix20220705(unitOfWork);
            //await Fix20220720(unitOfWork);
            //await Fix202200808(unitOfWork);
        }

        //public static async Task<bool> Fix20220705(IUnitOfWork unitOfWork)
        //{
        //    var saveMe = false;

        //    var core = await unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Name == "Delaneys.space");
        //    if (!core.IsDemo)
        //    {
        //        saveMe = true;
        //        core.IsDemo = true;
        //    }

        //    core = await unitOfWork.BusinessClients.SingleOrDefaultAsync(x => x.Name == "Lion's Heart");
        //    if (!core.IsDemo)
        //    {
        //        saveMe = true;
        //        core.IsDemo = true;
        //    }

        //    return saveMe;
        //}

        //public static async Task<bool> Fix20220720(IUnitOfWork unitOfWork)
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Fix 20220720 Start");

        //    var name = "Reseller Agency";
        //    var agency = await unitOfWork.BusinessClients.GetByNameAsync(name);

        //    if (agency == null)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("Fix 20220720 Failed");
        //        Console.ResetColor();

        //        return false;
        //    }


        //    name = "Delaneys.space";
        //    var client1 = await unitOfWork.BusinessClients.GetByNameAsync(name);

        //    if (client1 == null)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("Fix 20220720 Failed");
        //        Console.ResetColor();

        //        return false;
        //    }


        //    name = "Lion's Heart";
        //    var client2 = await unitOfWork.BusinessClients.GetByNameAsync(name);

        //    if (client2 == null)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("Fix 20220720 Failed");
        //        Console.ResetColor();

        //        return false;
        //    }


        //    if (!string.IsNullOrEmpty(client1.AgencyId) && !string.IsNullOrEmpty(client2.AgencyId))
        //    {
        //        Console.WriteLine("Fix 20220720 Change already made");
        //        Console.ResetColor();
        //        return true;
        //    }


        //    client1.AgencyId = agency.Id;
        //    client2.AgencyId = agency.Id;

        //    var message = "Fix 20220720 Completed";
        //    unitOfWork.Log(message, Severity.Information);
        //    await unitOfWork.CompleteAsync();

        //    Console.WriteLine(message);
        //    Console.ResetColor();

        //    return true;
        //}

        //public static async Task<bool> Fix202200808(IUnitOfWork unitOfWork)
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Fix 202200808 Start");

        //    bool isChanged = false;

        //    if (await UpdatePKAsync("Afghanistan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Aland Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Albania", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Algeria", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("American Samoa", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Andorra", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Angola", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Anguilla", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Antarctica", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Antigua And Barbuda", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Argentina", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Armenia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Aruba", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Australia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Austria", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Azerbaijan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Bahamas", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Bahrain", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Bangladesh", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Barbados", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Belarus", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Belgium", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Belize", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Benin", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Bermuda", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Bhutan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Bolivia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Bosnia and Herzegovina", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Botswana", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Brazil", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("British Indian Ocean Territory", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Brunei Darussalam", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Bulgaria", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Burkina Faso", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Burundi", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Cambodia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Cameroon", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Canada", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Cape Verde", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Cayman Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Central African Republic", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Chad", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Chile", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("China", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Christmas Island", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Cocos (Keeling) Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Colombia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Comoros", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Congo", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Congo, the Democratic Republic of the", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Cook Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Costa Rica", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Cote d'Ivoire", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Croatia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Curacao", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Cyprus", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Czech Republic", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Denmark", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Djibouti", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Dominica", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Dominican Republic", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Ecuador", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Egypt", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("El Salvador", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Equatorial Guinea", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Eritrea", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Estonia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Ethiopia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Falkland Islands (Malvinas)", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Faroe Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Fiji", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Finland", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("France", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("French Guiana", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("French Polynesia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("French Southern Territories", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Gabon", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Gambia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Georgia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Germany", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Ghana", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Gibraltar", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Greece", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Greenland", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Grenada", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Guadeloupe", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Guam", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Guatemala", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Guernsey", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Guinea", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Guinea-Bissau", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Guyana", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Haiti", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Heard Island and McDonald Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Holy See (Vatican City State)", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Honduras", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Hong Kong", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Hungary", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Iceland", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("India", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Indonesia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Iraq", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Ireland", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Isle of Man", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Israel", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Italy", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Jamaica", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Japan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Jersey", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Jordan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Kazakhstan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Kenya", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Kiribati", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Korea, Republic of", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Kosovo", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Kuwait", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Kyrgyzstan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Lao People's Democratic Republic", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Latvia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Lebanon", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Lesotho", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Liberia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Libyan Arab Jamahiriya", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Liechtenstein", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Lithuania", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Luxembourg", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Macao", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Macedonia, the former Yugoslav Republic of", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Madagascar", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Malawi", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Malaysia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Maldives", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Mali", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Malta", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Marshall Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Martinique", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Mauritania", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Mauritius", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Mayotte", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Mexico", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Micronesia, Federated States of", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Moldova, Republic of", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Monaco", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Mongolia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Montenegro", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Montserrat", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Morocco", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Mozambique", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Myanmar", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Namibia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Nauru", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Nepal", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Netherlands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Netherlands Antilles", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("New Caledonia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("New Zealand", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Nicaragua", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Niger", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Nigeria", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Niue", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Norfolk Island", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Northern Mariana Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Norway", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Oman", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Pakistan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Palau", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Palestinian Territory, Occupied", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Panama", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Papua New Guinea", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Paraguay", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Peru", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Philippines", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Pitcairn", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Poland", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Portugal", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Puerto Rico", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Qatar", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Reunion", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Romania", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Russian Federation", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Rwanda", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Saint Barthelemy", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Saint Helena", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Saint Kitts and Nevis", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Saint Lucia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Saint Martin (French part)", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Saint Pierre and Miquelon", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Saint Vincent and the Grenadines", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Samoa", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("San Marino", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Sao Tome and Principe", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Saudi Arabia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Senegal", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Serbia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Seychelles", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Sierra Leone", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Singapore", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Sint Maarten", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Slovakia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Slovenia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Solomon Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Somalia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("South Africa", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("South Georgia and the South Sandwich Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("South Sudan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Spain", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Sri Lanka", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Suriname", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Svalbard and Jan Mayen", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Swaziland", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Sweden", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Switzerland", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Taiwan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Tajikistan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Tanzania, United Republic of", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Thailand", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Timor-Leste", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Togo", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Tokelau", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Tonga", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Trinidad and Tobago", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Tunisia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Turkey", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Turkmenistan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Turks and Caicos Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Tuvalu", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Uganda", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Ukraine", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("United Arab Emirates", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("United Kingdom", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("United States", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("United States Minor Outlying Islands", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Uruguay", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Uzbekistan", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Vanuatu", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Venezuela", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Viet Nam", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Virgin Islands, British", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Virgin Islands, U.S.", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Western Sahara", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Yemen", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Zambia", unitOfWork)) isChanged = true;
        //    if (await UpdatePKAsync("Zimbabwe", unitOfWork)) isChanged = true;

        //    if (!isChanged)
        //        return false;

        //    var message = "Fix 202200808 Completed";
        //    unitOfWork.Log(message, Severity.Information);
        //    await unitOfWork.CompleteAsync();

        //    Console.WriteLine(message);
        //    Console.ResetColor();

        //    return true;
        //}

        public static async Task<bool> UpdatePKAsync(string name, IUnitOfWork unitOfWork)
        {
            var core = await unitOfWork.SharedCountries.GetByNameAsync(name);

            if (core.Id == core.Name)
                return false;


            var clients = await unitOfWork.BusinessClients.FindAsync(x => x.CountryId == core.Id);
            foreach (var client in clients)
                client.CountryId = core.Name;

            core.Id = core.Name;

            return true;
        }
    }
}
