if (!SilkFlo)
    SilkFlo = {};

if (!SilkFlo.Models2)
    SilkFlo.Models2 = {};

if (!SilkFlo.Models2.Business)
    SilkFlo.Models2.Business = {};

SilkFlo.Models2.Business.Client = {


    // SilkFlo.Models2.Business.Client.GetModalParent
    GetModalParent: function ()
    {
        //const id = 'Business.Client.Account';
        //const id = 'Settings.Tenants.Modal';
        const id = 'Modal.Business.Client';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.Models2.Business.Client.GetModalParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },

    // SilkFlo.Models2.Business.Client.GetParent
    GetParent: function ()
    {
        const id = 'Business.Client.Account';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent) {
            const logPrefix = 'SilkFlo.Models2.Business.Client.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },

    UpdateEmailSuffix: function (inputElement)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Client.UpdateEmailSuffix: ';

        // Guard Clause
        if (!inputElement)
        {
            console.log(logPrefix + 'inputElement parameter missing');
            return;
        }

        const parent = SilkFlo.Models2.Business.Client.GetModalParent ();


        const name = 'Modal.Business.Client.AccountOwnerEmail_Suffix';
        const target = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!target) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        if (inputElement.value)
        {
            target.innerHTML = `@${inputElement.value}`;
        }
        else
        {
            target.innerHTML = 'Website address missing';
        }
    },

    TypeIdOnChange: function (element)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Client.TypeIdOnChange: ';

        const parent = SilkFlo.Models2.Business.Client.GetModalParent();

        // Guard Clause
        if (!parent)
            return;


        let name = 'Modal.Business.Client.IsDemo_Container';
        const elementDiscount = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementDiscount)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }




        name = 'Modal.Business.Client.ReferrerDiscountId_Container';
        const elementReferrer = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementReferrer)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'Modal.Business.Client.ResellerDiscountId_Container';
        const elementReseller = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementReseller)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'Modal.Business.Client.DiscountMessage';
        const elementDiscountMessage = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementDiscountMessage) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = 'Modal.Business.Client.Subscription_Container';
        const elementSubscription = parent.querySelector(`[name="${name}"]`);


        if (element.value === 'ReferrerAgency41')
        {
            elementDiscount.style.display = 'none';
            elementReferrer.style.display = '';
            elementReseller.style.display = 'none';
            elementDiscountMessage.style.display = '';

            if (elementSubscription)
                elementSubscription.style.display = 'none';
        }
        else if (element.value === 'ResellerAgency45')
        {
            elementDiscount.style.display = 'none';
            elementReferrer.style.display = 'none';
            elementReseller.style.display = '';
            elementDiscountMessage.style.display = '';
            if (elementSubscription)
                elementSubscription.style.display = 'none';
        }
        else
        {
            elementDiscount.style.display = '';
            elementReferrer.style.display = 'none';
            elementReseller.style.display = 'none';
            elementDiscountMessage.style.display = 'none';
            if (elementSubscription)
                elementSubscription.style.display = 'flex';
        }
    },


    // Save details for signed in tenant or agency
    // SilkFlo.Models2.Business.Client.Save
    Save: function ()
    {
        const parent = SilkFlo.Models2.Business.Client.GetParent();

        // Guard Clause
        if (!parent)
            return;


        const model = SilkFlo.Models.Business.Client.GetModelFromParent
        (
            parent,
               ['Id',
                'Name',
                'Address1',
                'Address2',
                'City',
                'State',
                'PostCode',
                'CountryId',
                'AverageWorkingDay',
                'AverageWorkingHour',
                'AccountOwnerId',
                'IndustryId',
                'ReceiveMarketing',
                'AllowGuestSignIn']);


        // Guard Clause
        if (!model)
        {
            const logPrefix = 'SilkFlo.Models2.Business.Client.Save: ';
            console.log(logPrefix + 'model missing');
            return;
        }


        const url = '/api/Models/Business/Client/Save';
        SilkFlo.Models.Business.Client.Save(
            model,
            SilkFlo.Models2.Business.Client.Save_Callback,
            SilkFlo.DataAccess.Feedback,
            parent.id,
            url);
    },


    // SilkFlo.Models2.Business.Client.Save_Callback
    Save_Callback: function ()
    {
        SilkFlo.ShowDashboard();
        SilkFlo.SideBar.SetWorkspace ();
    },


    // SilkFlo.Models2.Business.Client.AffiliateURL
    CopyAffiliateURL: function () {
        const parent = SilkFlo.Models2.Business.Client.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const model = SilkFlo.Models.Business.Client.GetModelFromParent
        (
            parent,
            ['AffiliateURL']);

        model.AffiliateURL = model.AffiliateURL.replaceAll('&lt;', '<');
        model.AffiliateURL = model.AffiliateURL.replaceAll('&gt;', '>');

        navigator.clipboard.writeText(model.AffiliateURL);
    },

    // SilkFlo.Models2.Business.Client.CopyAffiliateLink
    CopyAffiliateLink: function ()
    {
        const parent = SilkFlo.Models2.Business.Client.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const model = SilkFlo.Models.Business.Client.GetModelFromParent
        (
            parent,
            ['AffiliateLink']);

        model.AffiliateLink = model.AffiliateLink.replaceAll('&lt;','<');
        model.AffiliateLink = model.AffiliateLink.replaceAll('&gt;', '>');

        navigator.clipboard.writeText(model.AffiliateLink);
    }
};