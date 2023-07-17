if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

SilkFlo.ViewModels.UserProfile = {

    // SilkFlo.ViewModels.UserProfile.GetParent
    GetParent: function () {

        const id = 'UserProfile.User';
        const parent = document.getElementById(id);

        // Guard Clause
        if (parent)
            return parent;


        const logPrefix = 'SilkFlo.ViewModels.UserProfile.GetParent: ';
        console.log(`${logPrefix}element with id ${id} missing`);
        return null;
    },

    // SilkFlo.ViewModels.UserProfile.Validate
    Validate: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.UserProfile.Validate: ';

        const parent = SilkFlo.ViewModels.UserProfile.GetParent();

        // Guard Clause
        if (!parent)
            return false;

        const id = 'UserProfile.User.EmailNewPrefix_InvalidFeedback';
        const invalidFeedback = document.getElementById(id);

        // Guard Clause
        if (!invalidFeedback)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return false;
        }

        SilkFlo.ViewModels.UserProfile.ClearErrorMessages();

        const model = SilkFlo.Models.User.GetModelFromParent(
            parent,
            [
                'Id',
                'About',
                'DepartmentId',
                'EmailNewPrefix',
                'EmailPrefix',
                'FirstName',
                'LastName',
                'LocationId',
                'ManagerId'],
            'UserProfile.User.');

        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'model missing');
            return false;
        }



        let isValid = true;
        if (!model.EmailNewPrefix)
        {
            invalidFeedback.innerHTML = 'The email address is not present.';

            if (!parent.ModelElements)
            {
                console.log(logPrefix + 'parent.ModelElements missing');
                return false;
            }

            if (!parent.ModelElements.EmailNewPrefix)
            {
                console.log(logPrefix + 'parent.ModelElements.EmailNewPrefix missing');
                return false;
            }


            parent.ModelElements.EmailNewPrefix.classList.add('is-invalid');
            isValid = false;
        }
        else
        {
            parent.ModelElements.EmailNewPrefix.classList.remove('is-invalid');
        }


        let email = model.EmailNewPrefix;

        if (email)
        {
            if (email.indexOf ( '@' ) === -1)
                email += '@a.a';

            const message = SilkFlo.Models2.User.ValidateEmail(email);
            if (message) {
                invalidFeedback.innerHTML = message;
                parent.ModelElements.EmailNewPrefix.classList.add('is-invalid');
                invalidFeedback.style.display = 'block';
                isValid = false;
            }
            else {
                parent.ModelElements.EmailNewPrefix.classList.remove('is-invalid');
                invalidFeedback.style.display = '';
            }
        }
        else
        {
            invalidFeedback.innerHTML = 'Email address missing.';
            parent.ModelElements.EmailNewPrefix.classList.add('is-invalid');
            invalidFeedback.style.display = 'block';
            isValid = false;
        }





        if (!model.FirstName
         && !model.LastName)
        {
            SilkFlo.ViewModels.UserProfile.AddErrorMessage('Both the first and last names are not present.', 'User_errors');
            isValid = false;
        }

        return isValid;
    },


    // SilkFlo.ViewModels.UserProfile.ClearErrorMessages
    ClearErrorMessages: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.UserProfile.AddErrorMessage: ';

        const elementId = 'UserProfile.User.Errors';
        const element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(logPrefix + 'element with id ' + elementId + ' missing');
            return;
        }

        element.innerHTML = '';
    },


    // SilkFlo.ViewModels.UserProfile.AddErrorMessage
    AddErrorMessage: function (message, elementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.UserProfile.AddErrorMessage: ';

        const element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(logPrefix + 'element with id ' + elementId + ' missing');
            return;
        }


        const node = document.createElement('p');
        node.classList.add('text-danger');
        node.innerHTML = message;

        element.appendChild(node);
    },


    // SilkFlo.ViewModels.UserProfile.ConfirmEmailChange_Click
    ConfirmEmailChange_Click: function ()
    {
        window.$('#MessageBoxUserProfileEmailChangeConfirmation').modal('hide');
        SilkFlo.ViewModels.UserProfile.CanChangeEmail = true;
        SilkFlo.ViewModels.UserProfile.Post();
    },

    // SilkFlo.ViewModels.UserProfile.PasswordChangedConfirmation_Click
    PasswordChangedConfirmation_Click: function ()
    {
        window.$('#ModalChangePassword').modal('hide');
        window.$('#ModalUserProfile').modal('show');
    },




    // SilkFlo.ViewModels.UserProfile.Save
    Save: function ()
    {
        SilkFlo.ViewModels.UserProfile.CanChangeEmail = false;


        if (!SilkFlo.ViewModels.UserProfile.Validate())
            return;


        SilkFlo.ViewModels.UserProfile.Post();
    },


    // SilkFlo.ViewModels.UserProfile.Post
    Post: function ()
    {
        const parent = document.getElementById('UserProfile.User');

        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.UserProfile.Post: ';
            console.log(logPrefix + 'element with id UserProfile.User missing');
            return;
        }

        const model = SilkFlo.Models.User.GetModelFromParent(
            parent,
            [
                'Id',
                'About',
                'DepartmentId',
                'EmailNewPrefix',
                'EmailPrefix',
                'FirstName',
                'JobTitle',
                'LastName',
                'LocationId',
                'ManagerId'],
            'UserProfile.User.');


        if (model.EmailPrefix !== model.EmailNewPrefix
            && !SilkFlo.ViewModels.UserProfile.CanChangeEmail)
        {
            window.$('#MessageBoxUserProfileEmailChangeConfirmation').modal('show');
            return;
        }


        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.ViewModels.UserProfile.Post_Callback,
            SilkFlo.ViewModels.UserProfile.PostFailed_Callback,
            parent.id,
            '/api/User/PostProfile',
            'POST');
    },

    // SilkFlo.ViewModels.UserProfile.Post_Callback
    Post_Callback: function ()
    {
        SilkFlo.ViewModels.User.UpdateUIComponents ();

        // Close modal
        window.$ ( '#ModalUserProfile' )
            .modal ( 'hide' );
    },


    // SilkFlo.ViewModels.UserProfile.PostFailed_Callback
    PostFailed_Callback: function (feedback, parentId)
    {
        console.log('SilkFlo.ViewModels.UserProfile.Save_Callback');
        window.$('#ModalUserProfile').modal('show');
        SilkFlo.DataAccess.Feedback(feedback, parentId);
    },


    ChangePassword:
    {
        // SilkFlo.ViewModels.UserProfile.ChangePassword.GetParent
        GetParent: function () {

            const id = 'Modal.ChangePassword';
            const parent = document.getElementById(id);

            // Guard Clause
            if (parent)
                return parent;


            const logPrefix = 'SilkFlo.ViewModels.UserProfile.ChangePassword.GetParent: ';
            console.log(`${logPrefix}element with id ${id} missing`);
            return null;
        },

        // SilkFlo.ViewModels.UserProfile.ChangePassword.ToggleButtonState
        ToggleButtonState: function ()
        {
            const logPrefix = 'SilkFlo.ViewModels.UserProfile.ChangePassword.ToggleButtonState: ';


            const id = 'User.ChangePasswordButton';
            const btn = document.getElementById(id);

            // Guard Clause
            if (!btn) {
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }


            const parent = SilkFlo.ViewModels.UserProfile.ChangePassword.GetParent();
            if (!parent)
                return;


            let name = 'User.OldPassword';
            const elementOldPassword = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementOldPassword) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            name = 'User.Password';
            const elementPassword = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementPassword) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            name = 'User.ConfirmPassword';
            const elementConfirmPassword = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementConfirmPassword) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            //console.log('elementOldPassword.value: ' + elementOldPassword.value);
            //console.log('elementPassword.value: ' + elementPassword.value);
            //console.log('elementConfirmPassword.value: ' + elementConfirmPassword.value);


            if (elementOldPassword.value.length > 0
                && elementPassword.value === elementConfirmPassword.value)
            {
                if (window.PasswordValid)
                    btn.classList.remove ( 'disabled' );
                else
                    btn.classList.add ( 'disabled' );
            }
            else
                btn.classList.add('disabled');
        },

        // SilkFlo.ViewModels.UserProfile.ChangePassword.SetMessage
        SetMessage: function (innerHtml) {
            const logPrefix = 'SilkFlo.ViewModels.UserProfile.ChangePassword.SetMessage: ';
            const parent = SilkFlo.ViewModels.UserProfile.ChangePassword.GetParent();
            if (!parent)
                return;

            const name = 'Message';
            const element = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            element.innerHTML = innerHtml;
        },



        // SilkFlo.ViewModels.UserProfile.ChangePassword.Click
        Click: function ()
        {
            if (!SilkFlo.ViewModels.UserProfile.ChangePassword.Validate())
                return;

            SilkFlo.ViewModels.UserProfile.ChangePassword.Save();
        },


        // SilkFlo.ViewModels.UserProfile.ChangePassword.Validate
        Validate: function ()
        {
            const parent = SilkFlo.ViewModels.UserProfile.ChangePassword.GetParent();
            if (!parent)
                return false;

            const model = SilkFlo.Models.User.GetModelFromParent(
                parent,
                [
                    'OldPassword',
                    'Password',
                    'ConfirmPassword'
            ]);


            let isValid = true;
            SilkFlo.ViewModels.UserProfile.ChangePassword.ClearErrorMessages();

            if (!model.OldPassword)
            {
                isValid = false;
                parent.ModelElements.OldPassword.classList.add('is-invalid');
            }



            if (!model.Password)
            {
                isValid = false;
                parent.ModelElements.Password.classList.add('is-invalid');
            }



            if (!model.ConfirmPassword)
            {
                isValid = false;
                parent.ModelElements.ConfirmPassword.classList.add('is-invalid');
            }



            if (model.Password !== model.ConfirmPassword)
            {
                SilkFlo.ViewModels.UserProfile.ChangePassword.SetMessage('<span class="text-danger">The new and confirm passwords do not match.</span>');
                isValid = false;
            }

            return isValid;
        },




        // SilkFlo.ViewModels.UserProfile.ChangePassword.ClearErrorMessages
        ClearErrorMessages: function ()
        {
            const parent = SilkFlo.ViewModels.UserProfile.ChangePassword.GetParent();
            if (!parent)
                return;

            SilkFlo.Models.User.GetModelFromParent(parent, ['OldPassword', 'Password', 'ConfirmPassword']);

            parent.ModelElements.OldPassword.classList.remove('is-invalid');
            parent.ModelElements.Password.classList.remove('is-invalid');
            parent.ModelElements.ConfirmPassword.classList.remove('is-invalid');


            SilkFlo.ViewModels.UserProfile.ChangePassword.SetMessage ( '' );
        },


        // SilkFlo.ViewModels.UserProfile.ChangePassword.Save
        Save: function ()
        {
            const parent = SilkFlo.ViewModels.UserProfile.ChangePassword.GetParent();
            if (!parent)
                return;

            // Get the model
            const model = SilkFlo.Models.User.GetModelFromParent(
                parent,
                [
                    'OldPassword',
                    'Password',
                    'ConfirmPassword',
                    'Id']);


            SilkFlo.Models.Abstract.Save(
                model,
                SilkFlo.ViewModels.UserProfile.ChangePassword.Save_Callback,
                SilkFlo.DataAccess.Feedback,
                'Modal.ChangePassword',
                '/api/User/PostPasswordChange',
                'POST');
        },

        // SilkFlo.ViewModels.UserProfile.ChangePassword.Save_Callback
        Save_Callback: function ()
        {
            SilkFlo.ViewModels.User.UpdateUIComponents();

            // Close the modal
            window.$('#MessageBoxPasswordChangedConfirmation').modal('show');
        }
    }
};