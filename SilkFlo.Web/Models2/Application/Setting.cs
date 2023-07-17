using System;
using System.Threading.Tasks;
using SilkFlo.Data.Core;
using static System.Int32;

namespace SilkFlo.Web.Models.Application
{
    public partial class Setting
    {
        private readonly IUnitOfWork _unitOfWork;
        public Setting(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? 
                          throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task CreateDefaultTrialPeriod()
        {
            const int trialPeriod = 30;
            var id = Enumerators.Setting.TrialPeriod.ToString();
            var setting = await _unitOfWork.ApplicationSettings.GetAsync(id);
            if (setting == null)
            {
                setting = new Data.Core.Domain.Application.Setting
                {
                    Value = trialPeriod.ToString()
                };
                await _unitOfWork.AddAsync(setting);
                setting.Id = id;
            }

        }

        public async Task<string> GetAsync(
            Enumerators.Setting name,
            string defaultValue = "")
        {
            var id = name.ToString();
            var setting = await _unitOfWork.ApplicationSettings.GetAsync(id);

            return setting == null ? 
                defaultValue : 
                setting.Value;
        }

        public async Task<bool> GetPracticeAccountCanSignIn()
        {
            var id = Enumerators.Setting.PracticeAccountCanSignIn.ToString();
            var practiceAccountCanSignIn = await _unitOfWork.ApplicationSettings.GetAsync(id);
            if (practiceAccountCanSignIn == null)
            {
                practiceAccountCanSignIn = new Data.Core.Domain.Application.Setting
                {
                    Value = "false"
                };

                await _unitOfWork.AddAsync(practiceAccountCanSignIn);
                practiceAccountCanSignIn.Id = id;
            }

            bool.TryParse(practiceAccountCanSignIn.Value, out var b);

            return b;
        }

        public async Task<string> GetPracticeAccountPassword()
        {
            var id = Enumerators.Setting.PracticeAccountPassword.ToString();
            var practiceAccountPassword = await _unitOfWork.ApplicationSettings.GetAsync(id);

            if (practiceAccountPassword != null)
                return practiceAccountPassword.Value;

            practiceAccountPassword = new Data.Core.Domain.Application.Setting
            {
                Value = "password1"
            };

            await _unitOfWork.AddAsync(practiceAccountPassword);
            practiceAccountPassword.Id = id;

            return practiceAccountPassword.Value;
        }

        public async Task<int> GetTrialPeriod()
        {
            var s = await GetAsync(Enumerators.Setting.TrialPeriod);
            TryParse(s, out var trialPeriod);
            return trialPeriod;
        }

        public async Task<string> GetTestEmailAccount()
        {
            var s = await GetAsync(Enumerators.Setting.TestEmailAccount);
            return s;
        }
    }
}