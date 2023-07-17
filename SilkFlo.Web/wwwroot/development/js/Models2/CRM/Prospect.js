if (!SilkFlo.Models2)
    SilkFlo.Models2 = {};

if (!SilkFlo.Models2.CRM)
    SilkFlo.Models2.CRM = {};

SilkFlo.Models2.CRM.Prospect = {
    Pipeline: '',


    // SilkFlo.Models2.CRM.Prospect.GetParent
    GetParent: function ()
    {
        const id = 'CRM.Prospect';
        const element = document.getElementById ( id );

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.Models2.CRM.Prospect.GetParent: ';
            console.log ( `${logPrefix}Element with id ${id} missing` );
            return null;
        }

        return element;
    },


    // SilkFlo.Models2.CRM.Prospect.Message
    Message: function (innerHtml)
    {
        const logPrefix = 'SilkFlo.Models2.CRM.Prospect.Message: ';


        const parent = SilkFlo.Models2.CRM.Prospect.GetParent ();

        if (!parent)
            return;

        const name = 'Message';
        let element = parent.querySelector ( `[name="${name}"]` );
        if (element)
        {
            element.innerHTML = innerHtml;
            return;
        }


        if (!parent.nextElementSibling)
        {
            console.log ( `${logPrefix}No sibling element` );
            return;
        }


        const sibling = parent.nextElementSibling;


        element = sibling.querySelector ( `[name="${name}"]` );

        if (!element)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        element.innerHTML = innerHtml;
    },


    // SilkFlo.Models2.CRM.Prospect.CopyReCaptcha
    CopyReCaptcha: function ()
    {
        const logPrefix = 'SilkFlo.Models2.CRM.Prospect.CopyReCaptcha: ';

        const cls = 'grecaptcha-badge';
        const elements = document.getElementsByClassName(cls);

        // Guard Clause
        if (!elements || elements.length < 1) {
            console.log(`${logPrefix}Element with class ${cls} missing`);
            return;
        }
        const elementSource = elements[0];

        const parent = SilkFlo.Models2.CRM.Prospect.GetParent();

        if (!parent)
            return;

        const name = 'reCaptchaBadgeContainer';
        const elementTarget = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementTarget) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        const attributeStyle = elementSource.getAttribute('style');
        const attributeClass = elementSource.getAttribute('class');

        elementTarget.setAttribute('style', attributeStyle);
        elementTarget.setAttribute('class', attributeClass);
        elementTarget.innerHTML = elementSource.innerHTML;
        elementTarget.style.position = '';
        elementSource.style.display = 'none';
    },


    // SilkFlo.Models2.CRM.Prospect.SetPipeline
    SetPipeline: function (pipeline)
    {
        SilkFlo.Models2.CRM.Prospect.Pipeline = pipeline;
    },


    // SilkFlo.Models2.CRM.Prospect.TermsAgreedOnInput
    TermsAgreedOnInput: function (e)
    {
        const element = e.target;
        if (element.checked)
            element.classList.remove('is-invalid');
        else
            element.classList.add ( 'is-invalid' );
    },

    // SilkFlo.Models2.CRM.Prospect.Save
    Save: function (e)
    {
        grecaptcha.ready (
            function ()
            {
                grecaptcha.execute (
                        SilkFlo.GReCaptchaPublicKey,
                        {
                            action: 'signup'
                        } )
                    .then (
                        function (token)
                        {
                            $ ( '#hdnGoogleRecaptcha' )
                                .val ( token );
                            $ ( '#lblValue' )
                                .html ( token );


                            const parent = SilkFlo.Models2.CRM.Prospect.GetParent ();

                            if (!parent)
                                return;

                            const sibling = parent.nextElementSibling;
                            const name = 'submitButton';
                            const element = sibling.querySelector ( `[name="${name}"]` );

                            // Guard Clause
                            if (!element)
                            {
                                const logPrefix = 'SilkFlo.Models2.CRM.Prospect.Save: ';
                                console.log ( `${logPrefix}Element with name ${name} missing` );
                                return;
                            }

                            element.setAttribute (
                                'disabled',
                                '' );
                            element.innerHTML = '<span class="spinner-border spinner-border-sm" style="margin-right: 1rem;"></span>Submitting...';

                            SilkFlo.Models2.CRM.Prospect.Message ( '<span class="text-info">Validating email address. Please wait.</span>' );

                            const model = SilkFlo.Models.Abstract.GetModelFromParent (
                                parent,
                                [
                                    'FirstName',
                                    'LastName',
                                    'PhoneNumber',
                                    'Email',
                                    'JobLevelId',
                                    'CompanyName',
                                    'CompanySizeId',
                                    'CountryId',
                                    'TermsAgreed'
                                ],
                                'CRM.Prospect.' );


                            model.ReCaptchaToken = token;
                            model.Pipeline = SilkFlo.Models2.CRM.Prospect.Pipeline;

                            SilkFlo.Models.Abstract.Save (
                                model,
                                SilkFlo.Models2.CRM.Prospect.IsValidate,
                                SilkFlo.Models2.CRM.Prospect.SaveFailedCallback,
                                parent.id,
                                '/api/CRM/Prospect/Save' );
                        } );
            } );
    },


    // SilkFlo.Models2.CRM.Prospect.SaveFailedCallback
    SaveFailedCallback: function (feedback, id)
    {
        SilkFlo.DataAccess.Feedback (
            feedback,
            id);


        const parent = SilkFlo.Models2.CRM.Prospect.GetParent();

        if (!parent)
            return;

        const sibling = parent.nextElementSibling;

        const name = 'submitButton';
        const element = sibling.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.Models2.CRM.Prospect.SaveFailedCallback: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.removeAttribute(
            'disabled');

        element.innerHTML = 'Submit';

    },

    EnableButton: function ()
    {
        const parent = SilkFlo.Models2.CRM.Prospect.GetParent ();

        if (!parent)
            return;


        const sibling = parent.nextElementSibling;
        const name = 'submitButton';
        const element = sibling.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.Models2.CRM.Prospect.EnableButton: ';
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        element.removeAttribute ( 'disabled' );
        element.innerHTML = 'Submit';
    },


    // SilkFlo.Models2.CRM.Prospect.IsValidate
    IsValidate: function (
        str,
        targetElementId)
    {
        SilkFlo.Models2.CRM.Prospect.EnableButton ();
        SilkFlo.DataAccess.Feedback (
            str,
            targetElementId );

        window.open (
                'https://calendly.com/alexander-silkflo/20min',
                '_blank' )
            .focus ();

        setTimeout (
            SilkFlo.Models2.CRM.Prospect.CloseModal,
            2000 );
    },




    // SilkFlo.Models2.CRM.Prospect.CloseModal
    CloseModal: function ()
    {
        const element = document.getElementById ( 'ModalCRMProspect' );
        const modal = bootstrap.Modal.getInstance ( element );
        modal.hide ();
    }
};