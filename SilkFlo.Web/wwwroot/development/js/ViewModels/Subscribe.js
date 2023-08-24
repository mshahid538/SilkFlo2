//Required: <script src="https://js.stripe.com/v3/"></script>

if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

SilkFlo.ViewModels.Subscribe = {

    Model: null,

    // SilkFlo.ViewModels.Subscribe.GetParent
    GetParent: function () {
        const id = 'subscribe';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return element;
    },


    // SilkFlo.ViewModels.Subscribe.ShowMessage
    ShowMessage: function (innerHtml)
    {
        const parent = this.GetParent();
        if (!parent)
            return;

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.ShowMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = innerHtml;
    },


    // SilkFlo.ViewModels.Subscribe.UpdateCoupon_OnInput
    UpdateCoupon_OnInput: function (e)
    {
        if (e.code === 'Enter')
            SilkFlo.ViewModels.Subscribe.UpdateCoupon ();
    },


    // SilkFlo.ViewModels.Subscribe.UpdateCoupon
    UpdateCoupon: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.UpdateCoupon: ';
        const parent = SilkFlo.ViewModels.Subscribe.GetParent();

        let name = 'Account.SignUp.CouponCode';
        let element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }
        SilkFlo.SetCookie('CouponName', element.value);


        name = 'Account.SignUp.PriceId';
        element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.SetCookie('PriceId', element.value);


        SilkFlo.DataAccess.GetObject(
            '/api/shop/Subscribe/GetCoupon',
            SilkFlo.ViewModels.Subscribe.CouponUpdateAmount,
            SilkFlo.ViewModels.Subscribe.NoCouponFeedback);
    },


    // SilkFlo.ViewModels.Subscribe.CouponUpdateAmount
    CouponUpdateAmount: function (str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.CouponUpdateAmount: ';


        if (!str)
            return;

        const coupon = JSON.parse(str);


        const parent = SilkFlo.ViewModels.Subscribe.GetParent();


        SilkFlo.ViewModels.Subscribe.UpdateOrderSummary
           (coupon,
            parent,
            'Account.SignUp.OrderSummary');



        let name = 'Account.SignUp.CouponCode';
        let element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.classList.remove('is-invalid');




        // Update message
        name = 'Account.SignUp.CouponCodeMessage';
        element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        element.innerHTML = 'Coupon Applied!';
        element.classList.remove('text-danger');
        element.classList.add('text-success');
        element.style.display = '';

    },


    // SilkFlo.ViewModels.Subscribe.NoCouponFeedBack
    NoCouponFeedback: function (str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.NoCouponFeedback: ';

        if (!str)
            return;

        const feedback = JSON.parse(str);


        // Guard Clause
        if (!feedback.message)
            return;


        const parent = SilkFlo.ViewModels.Subscribe.GetParent();



        SilkFlo.ViewModels.Subscribe.UpdateOrderSummary
        (null,
            parent,
            'Account.SignUp.OrderSummary');



        let name = 'Account.SignUp.CouponCode';
        let element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.classList.add('is-invalid');

        
        name = 'Account.SignUp.CouponCodeMessage';
        element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = feedback.message;
        element.classList.add ( 'text-danger' );
        element.classList.remove('text-success');
        element.style.display = '';
    },


    // SilkFlo.ViewModels.Subscribe.UpdateOrderSummary
    UpdateOrderSummary: function (
        coupon,
        parent,
        name)
    {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.UpdateOrderSummary: ';


        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        // Guard Clause
        if (!name) {
            console.log(`${logPrefix}name parameter missing`);
            return;
        }


        let orderSummaryParent = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!orderSummaryParent) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'OrderSummary.Price';
        let element = orderSummaryParent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        const price = element.value;




        let discount = 0;
        if (coupon)
        {
            discount = coupon.discountAmount;
        }



        name = 'OrderSummary.Symbol';
        element = orderSummaryParent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        const symbol = element.value + '&nbsp;';


        name = 'OrderSummary.Discount';
        element = orderSummaryParent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = '-&nbsp;' + symbol + discount.toLocaleString('en-US', { minimumFractionDigits: 2 });

        
        name = 'OrderSummary.DiscountRow';
        const elementRow = orderSummaryParent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementRow) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        if (discount)
        {
            elementRow.style.display = 'flex';
        }
        else
        {
            elementRow.style.display = 'none';
        }




        name = 'OrderSummary.Total';
        element = orderSummaryParent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        element.innerHTML = symbol + (price - discount).toLocaleString('en-US', { minimumFractionDigits: 2 });





        name = 'OrderSummary.TrialDays';
        element = orderSummaryParent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        if (coupon
         && coupon.trialDay)
        {
            const trialDays = coupon.trialDay;
            element.innerHTML = trialDays;
            element.style.display = '';
        }
        else
        {
            name = 'OrderSummary.TrialDaysDefault';
            const elementDefault = orderSummaryParent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementDefault) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            var value = elementDefault.value;
            if (value)
            {
                element.innerHTML = value;
            }
            else
            {
                element.style.display = 'none';
            }
        }
    },


    // SilkFlo.ViewModels.Subscribe.ToggleContinueButton
    ToggleContinueButton: function (
                              parent,
                              isEnabled)
    {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.ToggleContinueButton';

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        const name = 'Account.SignUp.AccountDetailsContinue';
        const btnElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!btnElement) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        if (isEnabled)
            SilkFlo.EnableButton(btnElement);
        else
            SilkFlo.DisableButton(btnElement);
    },


    // SilkFlo.ViewModels.Subscribe.ToggleSaveButton
    ToggleSaveButton: function (
                              parent,
                              isEnabled) {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.ToggleSaveButton';

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        const name = 'Account.SignUp.BillingAddress.Save';
        const btnElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!btnElement) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        if (isEnabled)
            SilkFlo.EnableButton(btnElement);
        else
            SilkFlo.DisableButton(btnElement);
    },


    // SilkFlo.ViewModels.Subscribe.ToggleCancelButton
    ToggleCancelButton: function (
                            parent,
                            isEnabled) {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.ToggleCancelButton';

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        const name = 'Account.SignUp.BillingAddress.Cancel';
        const btnElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!btnElement) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        if (isEnabled)
            SilkFlo.EnableButton(btnElement);
        else
            SilkFlo.DisableButton(btnElement);
    },


    // SilkFlo.ViewModels.Subscribe.ToggleAccountDetails
    ToggleAccountDetails: function (parent, isEnabled)
    {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.ToggleAccountDetails';

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }


        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            [
                'FirstName',
                'LastName',
                'Email',
                'EmailConfirmation',
                'Password',
                'Name',
                'Address1',
                'Address2',
                'City',
                'State',
                'PostCode',
                'CountryId',
                'Website',
                'TermsAgreed',
                'ReceiveMarketing' ],
            'Account.SignUp.',
            false);



        const length = parent.ModelElementsList.length;
        for (let i = 0; i < length; i++)
        {
            const element = parent.ModelElementsList[i];
            if (isEnabled)
                element.removeAttribute ( 'disabled' );
            else
                element.setAttribute (
                    'disabled',
                    '' );
        }
    },


    // SilkFlo.ViewModels.Subscribe.TermsAgreedOnInput
    TermsAgreedOnInput: function (e)
    {
        const element = e.target;
        if (element.checked)
            element.classList.remove('is-invalid');
        else
            element.classList.add('is-invalid');
    },


    // SilkFlo.ViewModels.Subscribe.BillingAddress
    BillingAddress: {

        // SilkFlo.ViewModels.Subscribe.BillingAddress.UndoModel
        UndoModel: null,

        // SilkFlo.ViewModels.Subscribe.BillingAddress.ModelElements
        ModelElements: null,

        // SilkFlo.ViewModels.Subscribe.BillingAddress.Edit
        Edit: function () {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.BillingAddress.Edit: ';

            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;


            let name = 'Account.SignUp.BillingAddress.Readonly';
            const elementReadonly = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementReadonly) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            name = 'Account.SignUp.BillingAddress.Edit';
            const elementEdit = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementEdit) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            SilkFlo.ViewModels.Subscribe.BillingAddress.UndoModel =
                SilkFlo.Models.Abstract.GetModelFromParent(
                    parent,
                    [
                        'ClientId',
                        'Name',
                        'Address1',
                        'Address2',
                        'City',
                        'State',
                        'PostCode',
                        'CountryId'],
                    'Account.SignUp.',
                    false);



            elementReadonly.style.display = 'none';
            elementEdit.style.display = '';

            SilkFlo.ViewModels.Subscribe.BillingAddress.ModelElements = parent.ModelElements;
        },


        // SilkFlo.ViewModels.Subscribe.BillingAddress.Save ();
        Save: function ()
        {
            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;



            SilkFlo.ViewModels.Subscribe.ToggleSaveButton(parent, false);
            SilkFlo.ViewModels.Subscribe.ToggleAccountDetails(parent, false);

            SilkFlo.ViewModels.Subscribe.ShowMessage('<span class="text-info">Validating details.</span>');



            SilkFlo.ViewModels.Subscribe.Model = SilkFlo.Models.Abstract.GetModelFromParent(
                parent,
                [
                    'ClientId',
                    'Name',
                    'Address1',
                    'Address2',
                    'City',
                    'State',
                    'PostCode',
                    'CountryId'],
                'Account.SignUp.',
                false);



            // Validate the sign up details.
            // NOTE: using SilkFlo.Models.Abstract.Save for convenience.
            SilkFlo.Models.Abstract.Save(
                SilkFlo.ViewModels.Subscribe.Model,
                SilkFlo.ViewModels.Subscribe.BillingAddress.Save_Callback,
                SilkFlo.ViewModels.Subscribe.BillingAddress.SaveFailed_Callback,
                parent.id,
                '/api/shop/Subscribe/SaveBillingAddress');
        },


        // SilkFlo.ViewModels.Subscribe.BillingAddress.Save_Callback
        Save_Callback: function ()
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.BillingAddress.Save_Callback';

            SilkFlo.ViewModels.Subscribe.ShowMessage('');

            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;



            let str = '';
            if (SilkFlo.ViewModels.Subscribe.Model.Name)
                str += SilkFlo.ViewModels.Subscribe.Model.Name;

            if (SilkFlo.ViewModels.Subscribe.Model.Address1)
                str += `<br/>${SilkFlo.ViewModels.Subscribe.Model.Address1}`;

            if (SilkFlo.ViewModels.Subscribe.Model.Address2)
                str += `<br/>${SilkFlo.ViewModels.Subscribe.Model.Address2}`;

            if (SilkFlo.ViewModels.Subscribe.Model.City)
                str += `<br/>${SilkFlo.ViewModels.Subscribe.Model.City}`;

            if (SilkFlo.ViewModels.Subscribe.Model.State)
                str += `<br/>${SilkFlo.ViewModels.Subscribe.Model.State}`;

            if (SilkFlo.ViewModels.Subscribe.Model.PostCode)
                str += `<br/>${SilkFlo.ViewModels.Subscribe.Model.PostCode}`;

            if (SilkFlo.ViewModels.Subscribe.Model.CountryIdDisplayText)
                str += `<br/>${SilkFlo.ViewModels.Subscribe.Model.CountryIdDisplayText}`;


            let name = 'Account.SignUp.BillingAddress.ReadonlyText';
            let element = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            element.innerHTML = str;

            name = 'Account.SignUp.BillingAddress.Readonly';
            element = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            element.style.display = '';

            name = 'Account.SignUp.BillingAddress.Edit';
            element = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            element.style.display = 'none';

            SilkFlo.DataAccess.ResetFeedback(
                parent,
                [
                    'Name',
                    'Address1',
                    'Address2',
                    'City',
                    'State',
                    'PostCode',
                    'CountryId'],
                'Account.SignUp.');



            SilkFlo.ViewModels.Subscribe.ToggleSaveButton(parent, true);
            SilkFlo.ViewModels.Subscribe.ToggleAccountDetails(parent, true);


            SilkFlo.ViewModels.Subscribe.Stripe.CreateSetupIntent();
        },


        // SilkFlo.ViewModels.Subscribe.BillingAddress.SaveFailed_Callback
        SaveFailed_Callback: function (
            feedback,
            parentId)
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.BillingAddress.SaveFailed_Callback: ';


            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;






            SilkFlo.ViewModels.Subscribe.ShowMessage('');


            SilkFlo.ViewModels.Subscribe.ToggleSaveButton(parent, true);
            SilkFlo.ViewModels.Subscribe.ToggleAccountDetails(parent, true);


            SilkFlo.DataAccess.Feedback(
                feedback,
                parentId);
        },

        // SilkFlo.ViewModels.Subscribe.BillingAddress.Cancel
        Cancel: function ()
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.BillingAddress.Cancel: ';


            // Guard Clause
            if (!SilkFlo.ViewModels.Subscribe.BillingAddress.UndoModel) {
                console.log(logPrefix + 'SilkFlo.ViewModels.Subscribe.BillingAddress.UndoModel missing');
                return;
            }

            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;


            let name = 'Account.SignUp.BillingAddress.Readonly';
            const elementReadonly = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementReadonly) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            name = 'Account.SignUp.BillingAddress.Edit';
            const elementEdit = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementEdit) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            const undoModel = SilkFlo.ViewModels.Subscribe.BillingAddress.UndoModel;
            const modelElements = SilkFlo.ViewModels.Subscribe.BillingAddress.ModelElements;

            modelElements.Name.value = undoModel.Name;
            modelElements.Address1.value = undoModel.Address1;
            modelElements.Address2.value = undoModel.Address2;
            modelElements.City.value = undoModel.City;
            modelElements.State.value = undoModel.State;
            modelElements.PostCode.value = undoModel.PostCode;
            modelElements.CountryId.value = undoModel.CountryId;

            elementReadonly.style.display = '';
            elementEdit.style.display = 'none';

            SilkFlo.DataAccess.ResetFeedback(
                parent,
                [
                    'Name',
                    'Address1',
                    'Address2',
                    'City',
                    'Styate',
                    'PostCode',
                    'CountryId'],
                'Account.SignUp.');

        }
    },


    // SilkFlo.ViewModels.Subscribe.AccountDetails
    AccountDetails:
    {
        // SilkFlo.ViewModels.Subscribe.AccountDetails.ContinueButton
        ContinueButton: null,


        // SilkFlo.ViewModels.Subscribe.AccountDetails.ValidateDomain
        ValidateDomain: function ()
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.AccountDetails.ValidateDomain: ';


            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;


            let name = 'Account.SignUp.Email';
            const elementEmail = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementEmail) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            name = 'Account.SignUp.Website';
            const elementWebsite = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementWebsite) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            name = 'EmailRule';
            const elementEmailRule = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementEmailRule) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            let isDomainValid = false;

            const email = elementEmail.value;
            if (email.length > 0) {

                if (email.indexOf ( '@' ) > -1)
                {
                    const parts = email.split('@');
                    if (parts.length > 1)
                    {
                        const emailDomain = parts[1];

                        if (elementWebsite.value.toLowerCase() === emailDomain.toLowerCase())
                            isDomainValid = true;
                    }
                }
            }

            if (isDomainValid)
            {
                // is valid
                elementEmailRule.classList.add('isValid');
                elementEmailRule.classList.remove('isInValid');
            }
            else
            {
                // is invalid
                elementEmailRule.classList.add('isInValid');
                elementEmailRule.classList.remove('isValid');
            }
        },

        // SilkFlo.ViewModels.Subscribe.AccountDetails.ValidateEmail
        ValidateEmail: function (
                           email1Name,
                           email2Name,
                           emailInvalidId,
                           urlSignIn) {
            const logPrefix = 'SilkFlo.Models2.User.ValidateEmailDetailed: ';


            // Guard Clause
            if (!email1Name) {
                console.log(`${logPrefix}email1Name parameter missing`);
                return;
            }

            // Guard Clause
            if (!email2Name) {
                console.log(`${logPrefix}email2Name parameter missing`);
                return;
            }

            // Guard Clause
            if (!emailInvalidId) {
                console.log(`${logPrefix}emailInvalidId parameter missing`);
                return;
            }

            // Guard Clause
            if (!urlSignIn) {
                console.log(`${logPrefix}urlSignIn parameter missing`);
                return;
            }


            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;

            let name = 'EmailRule';
            const elementEmailRule = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementEmailRule) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            name = 'Account.SignUp.Website';
            const elementWebsite = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementWebsite) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }






            if (SilkFlo.Compare(
                parent.id,
                email1Name,
                email2Name,
                emailInvalidId,
                'Confirm email does not match email.',
                true)) {

                const element = parent.querySelector(`[name="${email1Name}"]`);

                // Guard Clause
                if (!element) {
                    console.log(`${logPrefix}Element with name ${email1Name} missing`);
                    return;
                }

                SilkFlo.Models2.User.ValidateEmailDetailed(
                    element,
                    emailInvalidId,
                    urlSignIn);
            }


        },

        // SilkFlo.ViewModels.Subscribe.AccountDetails.Save_Click
        Save_Click: function (event)
        {
            // Guard Clause
            if (!event) {
                const logPrefix = 'SilkFlo.ViewModels.Subscribe.AccountDetails.Save_Click: ';
                console.log(`${logPrefix}event parameter missing`);
                return;
            }

            if (!window.PasswordValid)
            {
                SilkFlo.ViewModels.Subscribe.ShowMessage('<span class="text-warning">Password Invalid.</span>');
                return;
            }

            event.preventDefault();
            grecaptcha.ready(function () {
                grecaptcha.execute(
                    SilkFlo.GReCaptchaPublicKey,
                    { action: 'signup' }).then(function (token) {

                        const element = event.target;

                        SilkFlo.ViewModels.Subscribe.AccountDetails.ContinueButton = element;

                        const parent = SilkFlo.ViewModels.Subscribe.GetParent();
                        if (!parent)
                            return;

                        SilkFlo.ViewModels.Subscribe.ToggleAccountDetails(parent, false);
                        SilkFlo.ViewModels.Subscribe.ToggleContinueButton(parent, false);

                        SilkFlo.ViewModels.Subscribe.ShowMessage('<span class="text-info">Validating details.</span>');

                        SilkFlo.RemoveSpinner(element);

                        const model = SilkFlo.Models.Abstract.GetModelFromParent(
                            parent,
                            [
                                'FirstName',
                                'LastName',
                                'Email',
                                'EmailConfirmation',
                                'Password',
                                'Name',
                                'Address1',
                                'Address2',
                                'City',
                                'State',
                                'PostCode',
                                'CountryId',
                                'TermsAgreed',
                                'CountryId',
                                'Website'],
                            'Account.SignUp.',
                            false);

                        model.ReCaptchaToken = token;

                        // Validate the sign up details.
                        // NOTE: using SilkFlo.Models.Abstract.Save for convenience.
                        SilkFlo.Models.Abstract.Save(
                            model,
                            SilkFlo.ViewModels.Subscribe.AccountDetails.Save_Callback,
                            SilkFlo.ViewModels.Subscribe.AccountDetails.SaveInValid_Callback,
                            parent.id,
                            '/api/shop/Subscribe/ClientDetails/Post');
                });
            });
        },

        // SilkFlo.ViewModels.Subscribe.AccountDetails.Save_Callback
        Save_Callback: function (clientId)
        {
            try
            {
                const logPrefix = 'SilkFlo.ViewModels.Subscribe.AccountDetails.Save_Callback: ';

                // Guard Clause
                if (!clientId) {
                    console.log(`${logPrefix}clientId parameter missing`);
                    return;
                }

                // Guard Clause
                if (!SilkFlo.ViewModels.Subscribe.AccountDetails.ContinueButton)
                {
                    console.log ( `${logPrefix}SilkFlo.ViewModels.Subscribe.AccountDetails.ContinueButton missing` );
                    return;
                }


                SilkFlo.RemoveSpinner(SilkFlo.ViewModels.Subscribe.AccountDetails.ContinueButton);

                SilkFlo.ViewModels.Subscribe.ShowMessage ( '' );

                const parent = SilkFlo.ViewModels.Subscribe.GetParent ();
                if (!parent)
                    return;

                SilkFlo.ViewModels.Subscribe.ToggleAccountDetails (
                    parent,
                    false);



                let name = 'Account.SignUp.AccountDetailsContinue';
                const btnElement = parent.querySelector(`[name="${name}"]`);

                // Guard Clause
                if (!btnElement) {
                    console.log(`${logPrefix}Element with name ${name} missing`);
                    return;
                }


                name = 'Account.SignUp.ClientId';
                const elementClientId = parent.querySelector(`[name="${name}"]`);

                // Guard Clause
                if (!elementClientId) {
                    console.log(`${logPrefix}Element with name ${name} missing`);
                    return;
                }

                elementClientId.value = clientId;

                SilkFlo.RemoveSpinner ( btnElement );


                SilkFlo.DataAccess.ResetFeedback (
                    parent,
                    [
                        'FirstName',
                        'LastName',
                        'Email',
                        'EmailConfirmation',
                        'Password',
                        'Name',
                        'Address1',
                        'Address2',
                        'City',
                        'State',
                        'PostCode',
                        'CountryId',
                        'TermsAgreed',
                        'CountryId',
                        'Website'
                    ],
                    'Account.SignUp.');

                SilkFlo.ViewModels.Subscribe.Stripe.CreateSetupIntent();
            }
            catch (ex) {
                console.log(ex.message);
            }
        },


        // SilkFlo.ViewModels.Subscribe.AccountDetails.SaveInValid_Callback
        SaveInValid_Callback: function (feedback)
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.AccountDetails.SaveInValid_Callback: ';

            // Guard Clause
            if (!feedback) {
                console.log(`${logPrefix}feedback parameter missing`);
                return;
            }

            // Guard Clause
            if (!SilkFlo.ViewModels.Subscribe.AccountDetails.ContinueButton)
            {
                console.log(`${logPrefix}SilkFlo.ViewModels.Subscribe.AccountDetails.ContinueButton missing`);
                return;
            }

            SilkFlo.RemoveSpinner(SilkFlo.ViewModels.Subscribe.AccountDetails.ContinueButton);
            SilkFlo.ViewModels.Subscribe.ShowMessage('');

            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;

            SilkFlo.ViewModels.Subscribe.ToggleAccountDetails(parent, true);
            SilkFlo.ViewModels.Subscribe.ToggleContinueButton(parent, true);

            SilkFlo.DataAccess.ResetFeedback(
                parent,
                [
                    'FirstName',
                    'LastName',
                    'Email',
                    'EmailConfirmation',
                    'Password',
                    'Name',
                    'Address1',
                    'Address2',
                    'City',
                    'State',
                    'PostCode',
                    'CountryId',
                    'TermsAgreed',
                    'CountryId',
                    'Website'],
                'Account.SignUp.',
                false);


            SilkFlo.DataAccess.Feedback(
                feedback,
                parent.id);
        }
    },

    // SilkFlo.ViewModels.Subscribe.FreeTrial
    FreeTrial:
    {
        ContinueButton: null,

        // SilkFlo.ViewModels.Subscribe.FreeTrial.ToggleContinueButton
        ToggleContinueButton: function (
            parent,
            isEnabled)
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.FreeTrial.ToggleContinueButton';

            // Guard Clause
            if (!parent) {
                console.log(`${logPrefix}parent parameter missing`);
                return;
            }

            const name = 'AccountDetailsContinue';
            const btnElement = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!btnElement) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            if (isEnabled)
                SilkFlo.EnableButton(btnElement);
            else
                SilkFlo.DisableButton(btnElement);
        },

        // SilkFlo.ViewModels.Subscribe.FreeTrial.Click
        Click: function (event)
        {
            // Guard Clause
            if (!event) {
                const logPrefix = 'SilkFlo.ViewModels.Subscribe.FreeTrial.Click: ';
                console.log(`${logPrefix}event parameter missing`);
                return;
            }

            event.preventDefault();
            grecaptcha.ready(function () {
                grecaptcha.execute(
                    SilkFlo.GReCaptchaPublicKey,
                    { action: 'signup' }).then(function (token) {

                        let element = event.target;

                        SilkFlo.ViewModels.Subscribe.FreeTrial.ContinueButton = element;

                        const parent = SilkFlo.ViewModels.Subscribe.GetParent();
                        if (!parent)
                            return;

                        SilkFlo.ViewModels.Subscribe.ToggleAccountDetails(parent, false);
                        SilkFlo.ViewModels.Subscribe.FreeTrial.ToggleContinueButton(parent, false);

                        SilkFlo.ViewModels.Subscribe.ShowMessage('<span class="text-info">Validating details.</span>');

                        SilkFlo.RemoveSpinner(element);

                        const name = 'Account.SignUp.ClientId';
                        element = parent.querySelector(`[name="${name}"]`);

                        let model = {};
                        if (element && element.value)
                        {
                            model = SilkFlo.Models.Abstract.GetModelFromParent(
                                parent,
                                [
                                    'ClientId',
                                    'TermsAgreed',
                                    'ReceiveMarketing'],
                                'Account.SignUp.',
                                false);
                        }
                        else
                        {
                            model = SilkFlo.Models.Abstract.GetModelFromParent(
                                parent,
                                [
                                    'FirstName',
                                    'LastName',
                                    'Email',
                                    'EmailConfirmation',
                                    'Password',
                                    'Name',
                                    'Website',
                                    'TermsAgreed',
                                    'ReceiveMarketing'],
                                'Account.SignUp.',
                                false);
                        }


                        model.ReCaptchaToken = token;

                        // Validate the sign up details.
                        // NOTE: using SilkFlo.Models.Abstract.Save for convenience.
                        SilkFlo.Models.Abstract.Save(
                            model,
                            SilkFlo.ViewModels.Subscribe.FreeTrial.Callback,
                            SilkFlo.ViewModels.Subscribe.FreeTrial.InValid_Callback,
                            parent.id,
                            '/api/shop/Subscribe/FreeTrial/Post');
                    });
            });
        },

        // SilkFlo.ViewModels.Subscribe.FreeTrial.Callback
        Callback: function ()
        {
            window.location.href = '../Welcome';
        },


        // SilkFlo.ViewModels.Subscribe.FreeTrial.InValid_Callback
        InValid_Callback: function (feedback)
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.FreeTrial.InValid_Callback: ';

            // Guard Clause
            if (!feedback) {
                console.log(`${logPrefix}feedback parameter missing`);
                return;
            }

            // Guard Clause
            if (!SilkFlo.ViewModels.Subscribe.FreeTrial.ContinueButton) {
                console.log(`${logPrefix}SilkFlo.ViewModels.Subscribe.FreeTrial.ContinueButton missing`);
                return;
            }

            SilkFlo.RemoveSpinner(SilkFlo.ViewModels.Subscribe.FreeTrial.ContinueButton);

            SilkFlo.ViewModels.Subscribe.ShowMessage('');

            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;

            SilkFlo.ViewModels.Subscribe.ToggleAccountDetails(parent, true);
            SilkFlo.ViewModels.Subscribe.FreeTrial.ToggleContinueButton(parent, true);

            const name = 'Account.SignUp.ClientId';
            const element = parent.querySelector(`[name="${name}"]`);

            if (!element) {
                SilkFlo.DataAccess.ResetFeedback(
                    parent,
                    [
                        'FirstName',
                        'LastName',
                        'Email',
                        'EmailConfirmation',
                        'Password',
                        'Name',
                        'TermsAgreed',
                        'Website'
                    ],
                    'Account.SignUp.',
                    false);
            }


            SilkFlo.DataAccess.Feedback(
                feedback,
                parent.id);
        }
    },



    // SilkFlo.ViewModels.Subscribe.IsTermsAgreedChecked
    IsTermsAgreedChecked: function ()
    {
        const parent = SilkFlo.ViewModels.Subscribe.GetParent();
        if (!parent)
            return false;


        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            [
                'TermsAgreed'],
            'Account.SignUp.');


        if (parent.ModelElements.TermsAgreed.checked) {
            parent.ModelElements.TermsAgreed.classList.remove('is-invalid');
            return true;
        }

        parent.ModelElements.TermsAgreed.classList.add('is-invalid');
        return false;
    },


    // SilkFlo.ViewModels.Subscribe.Continue_Click
    Continue_Click: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Subscribe.Continue_Click: ';

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        if (!SilkFlo.ViewModels.Subscribe.IsTermsAgreedChecked())
            return;


        SilkFlo.AddSpinner ( element );

        const parent = SilkFlo.ViewModels.Subscribe.GetParent();
        if (!parent)
            return;




        const id = 'Account.SignUp.NotYou';
        const elementNotYou = document.getElementById(id);

        // Guard Clause
        if (!elementNotYou) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        elementNotYou.remove();


        const name = 'Account.SignUp.Fieldset';
        const elementFieldset = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementFieldset) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        elementFieldset.disabled = true;

        SilkFlo.ViewModels.Subscribe.Stripe.CreateSetupIntent();
    },


    // SilkFlo.ViewModels.Subscribe.Stripe
    Stripe: {

        //SilkFlo.ViewModels.Subscribe.Stripe.Object
        Object: null,

        //SilkFlo.ViewModels.Subscribe.Stripe.PaymentElement
        PaymentElement: null,

        // SilkFlo.ViewModels.Subscribe.Stripe.ShowMessage
        ShowMessage: function (innerHtml)
        {
            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;

            const name = 'MessagePayment';
            const element = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element) {
                const logPrefix = 'SilkFlo.ViewModels.Subscribe.Stripe.ShowMessage: ';
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            element.innerHTML = innerHtml;
        },



        // SilkFlo.ViewModels.Subscribe.Stripe.CreateSetupIntent
        CreateSetupIntent: function ()
        {
            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;

            const name = 'Account.SignUp.ClientId';
            const element = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element) {
                const logPrefix = 'SilkFlo.ViewModels.Subscribe.Stripe.CreateSetupIntent: ';
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            SilkFlo.DataAccess.GetObject(
                '/api/shop/CreateSetupIntent/ClientId/' + element.value,
                SilkFlo.ViewModels.Subscribe.Stripe.CreateSetupIntent_CallBack,
                SilkFlo.DataAccess.Feedback,
                parent.id);

            const form = document.getElementById('payment-form');

            if (!form)
                return;


            // Add an on submet listener on the form.
            form.addEventListener(
                'submit',
                SilkFlo.ViewModels.Subscribe.Stripe.Form_OnSubmit);
        },






        // SilkFlo.ViewModels.Subscribe.Stripe.CreateSetupIntent_CallBack
        CreateSetupIntent_CallBack: function (clientSecret)
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.Stripe.CreateSetupIntent_CallBack: ';

            // Guard Clause
            if (!clientSecret) {
                console.log(`${logPrefix}clientSecret parameter missing`);
                return;
            }


            // Guard Clause
            if (!SilkFlo.ViewModels.Subscribe.Stripe.Object) {
                console.log(`${logPrefix}SilkFlo.ViewModels.Subscribe.Stripe.Object parameter missing`);
                return;
            }


            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;


            const name = 'Account.SignUp.BillingAddressContinue';
            const elementContinue = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementContinue) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            SilkFlo.RemoveSpinner(elementContinue);


            const id = 'payment-form';
            const element = document.getElementById(id);

            // Guard Clause
            if (!element) {
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }

            element.style.display = '';


            const appearance =
            {
                theme: 'stripe',
                labels: 'floating'
            };

            const stripe = SilkFlo.ViewModels.Subscribe.Stripe.Object;

            const options = {
                clientSecret: clientSecret,
                appearance: appearance,
            };

            // Set up Stripe.js and Elements to use in checkout form, passing the client secret obtained in step 2
            SilkFlo.ViewModels.Subscribe.Stripe.StripeElements = stripe.elements(options);

            // Create and mount the Payment Element
            const paymentElement = SilkFlo.ViewModels.Subscribe.Stripe.StripeElements.create('payment');
            paymentElement.mount('#payment-element');
        },


        // SilkFlo.ViewModels.Subscribe.Stripe.StripeElements
        StripeElements: null,

        // SilkFlo.ViewModels.Subscribe.Stripe.Form_OnSubmit
        Form_OnSubmit: function (event)
        {
            const logPrefix = 'SilkFlo.ViewModels.Subscribe.Stripe.Form_OnSubmit: ';

            // Guard Clause
            if (!event) {
                console.log(`${logPrefix}event parameter missing`);
                return;
            }

            // Guard Clause
            if (!SilkFlo.ViewModels.Subscribe.Stripe.StripeElements) {
                console.log(`${logPrefix}SilkFlo.ViewModels.Subscribe.Stripe.StripeElements missing`);
                return;
            }

            const parent = SilkFlo.ViewModels.Subscribe.GetParent();
            if (!parent)
                return;


            const model = SilkFlo.Models.Abstract.GetModelFromParent(
                parent,
                [
                    'ClientId',
                    'PriceId'],
                'Account.SignUp.');




            const elements = SilkFlo.ViewModels.Subscribe.Stripe.StripeElements;
            const url = `https://localhost:5001/shop/SubscriptionComplete/clientId/${model.ClientId}/PriceId/${model.PriceId}`;

            event.preventDefault();
            const stripe = SilkFlo.ViewModels.Subscribe.Stripe.Object;
            const { error } = stripe.confirmSetup({
                elements,
                confirmParams: {
                    return_url: url
                }
            });

            if (error) {
                // This point will only be reached if there is an immediate error when
                // confirming the payment. Show error to your customer (for example, payment
                // details incomplete)
                const messageContainer = document.querySelector('#error-message');
                messageContainer.textContent = error.message;
            } else {
                // Your customer will be redirected to your `return_url`. For some payment
                // methods like iDEAL, your customer will be redirected to an intermediate
                // site first to authorize the payment, then redirected to the `return_url`.
            }
        }
    }
};