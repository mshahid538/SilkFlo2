if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};


if (!SilkFlo.ViewModels.Settings.PlatformSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup = {};


// CostSetup Namespace: Code to manage the content inside the CostSetup tab
SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup = {

    SubTabAutoSelector: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SubTabAutoSelector: ';


        const id = 'Settings.PlatformSetup';
        const parent = document.getElementById(id);

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        let name = 'Settings.PlatformSetup.CostSetup.SoftwareVendor.Tab';

        const path = window.location.pathname.toLowerCase();

        if (path.indexOf ( 'cost-setup' ) === -1)
            return;


        if (path.indexOf( 'initial-costs' ) > -1)
            name = 'Settings.PlatformSetup.CostSetup.InitialCosts.Tab';
        else if (path.indexOf('other-running-costs') > -1)
            name = 'Settings.PlatformSetup.CostSetup.OtherRunningCosts.Tab';
        else if (path.indexOf('running-costs') > -1)
            name = 'Settings.PlatformSetup.CostSetup.RunningCosts.Tab';



        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.TabBar.SelectorByElement (element);
    },
};