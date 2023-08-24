function CancelMessageResultYes()
{
    //ToDo: CancelMessageResultYes
    console.log('Test');
}

if (!SilkFlo.Models2)
    SilkFlo.Models2 = {};

if (!SilkFlo.Models2.Shop)
    SilkFlo.Models2.Shop = {};

SilkFlo.Models2.Shop.Subscription = {

    Id: null,
    ClientId: null,

    PopulateModal: function (clientId)
    {
        const logPrefix = 'SilkFlo.Models2.Shop.Subscription.PopulateModal: ';


        const modalId = '#modelViewSubscriptions';
        $(modalId)
            .modal('show');

        // Guard Clause
        if (!clientId)
        {
            console.log ( `${logPrefix}clientId parameter missing` );
            return;
        }

        const id = 'Settings.Subscriptions.Modal';
        const modal = document.getElementById ( id );
        if (!modal)
        {
            console.log ( `${logPrefix}Element with id "${id}" missing` );
            return;
        }


        SilkFlo.Models2.Shop.Subscription.ClientId = clientId;

        const url = `/api/shop/subscription/GetModal/clientId/${clientId}`;


        SilkFlo.DataAccess.UpdateElement (
            url,
            null,
            modal,
            null,
            'GET' );
    },



    Cancel: {

        Email: null,



        // SilkFlo.Models2.Shop.Subscription.Cancel.Click
        Click: function (
            clientId,
            email)
        {
            const logPrefix = 'SilkFlo.Models2.Shop.Subscription.Cancel.Click: ';

            // Guard Clause
            if (!clientId)
            {
                console.log(`${logPrefix}clientId parameter missing` );
                return;
            }

            // Guard Clause
            if (!email) {
                console.log(`${logPrefix}email parameter missing`);
                return;
            }



            SilkFlo.Models2.Shop.Subscription.ClientId = clientId;
            SilkFlo.Models2.Shop.Subscription.Cancel.Email = email;

            const accountOwnerEmailId = 'accountOwnerEmail';
            const accountOwnerEmail = document.getElementById(accountOwnerEmailId);

            // Guard Clause
            if (!accountOwnerEmail)
            {
                console.log(`${logPrefix}Element with id ${accountOwnerEmailId} missing`);
                return;
            }

            accountOwnerEmail.innerHTML = email;

            const message = window.$('#SettingsSubscriptionsCancelMessage');
            message.modal ( 'show' );
        },


        // SilkFlo.Models2.Shop.Subscription.Cancel.MessageResultYes
        MessageResultYes: function (targetId)
        {
            const url = `/api/Shop/Subscription/SendCancellationMessage`;
            console.log(url);

            SilkFlo.Models.Abstract.Save(
                SilkFlo.Models2.Shop.Subscription.ClientId,
                SilkFlo.Models2.Shop.Subscription.Cancel.SendCancellationMessage_Callback,
                SilkFlo.DataAccess.Feedback,
                targetId,
                url,
                SilkFlo.Models.Abstract.Verb.PUT);
        },

        // SilkFlo.Models2.Shop.Subscription.Cancel.SendCancellationMessage_Callback
        SendCancellationMessage_Callback: function (ignore, targetId)
        {
            const text = `<span class="text-info">Cancellation email sent to ${SilkFlo.Models2.Shop.Subscription.Cancel.Email}.</soan>`;
            SilkFlo.Models2.Shop.Subscription.DisplayMessage ( text, targetId );
        }
    },

    DisplayMessage: function (text, parentId)
    {
        const logPrefix = 'SilkFlo.Models2.Shop.Subscription.DisplayMessage: ';

        // Guard Clause
        if (!parentId)
        {
            console.log(`${logPrefix}targetId parameter missing`);
            return;
        }

        const parent = document.getElementById(parentId);

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${parentId} missing`);
            return;
        }

        const name = 'Message';
        const message = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!message)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        message.innerHTML = text;
    },

    SelectPrice_Click: function (priceId, referrerCode)
    {
        const logPrefix = 'SilkFlo.Models2.Shop.Subscription.SelectPrice_Click: ';
        console.log(logPrefix);

        // Guard Clause
        if (!priceId) {
            console.log(`${logPrefix}priceId parameter missing`);
            return;
        }

        const id = 'body';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        if (referrerCode)
            window.location.replace(`/shop/subscribe/priceId/${priceId}/referrerCode/${referrerCode}` );
        else
            window.location.replace ( `/shop/subscribe/priceId/${priceId}` );
    },

    // SilkFlo.Models2.Shop.Subscription.SelectPriceFailed_Callback
    SelectPriceFailed_Callback: function (feedback, parentId)
    {
        const logPrefix = 'SilkFlo.Models2.Shop.Subscription.SelectPriceFailed_Callback: ';


        const id = 'body';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        element.style.opacity = 1;
        element.innerHTML = '<div name="Message" style="margin: 1rem;"></div>';

        SilkFlo.DataAccess.Feedback(feedback, id);
    }
};