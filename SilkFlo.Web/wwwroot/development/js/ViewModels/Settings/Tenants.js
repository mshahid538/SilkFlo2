if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};


SilkFlo.ViewModels.Settings.Tenants = {
    Summary: {
        SelectedRow: null,
        IsDeleting: false,

        // SilkFlo.ViewModels.Settings.Tenants.Summary.GetParent
        GetParent: function ()
        {
            const id = 'Settings.Tenants';
            const parent = document.getElementById ( id );

            // Guard Clause
            if (!parent)
            {
                const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Summary.GetParent: ';
                console.log ( `${logPrefix}Element with id "${id}" missing` );
                return null;
            }

            return parent;
        },


        // SilkFlo.ViewModels.Settings.Tenants.Summary.SetMessage
        SetMessage: function (text)
        {
            const parent = SilkFlo.ViewModels.Settings.Tenants.Summary.GetParent ();

            if (!parent)
                return;

            const name = 'Message';
            const element = parent.querySelector ( `[name="${name}"]` );

            // Guard Clause
            if (!element)
            {
                const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Summary.SetMessage: ';
                console.log ( `${logPrefix}Element with name ${name} missing` );
                return;
            }

            element.innerHTML = text;
        },


        // SilkFlo.ViewModels.Settings.Tenants.Summary.UpdateView
        UpdateView: function ()
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Summary.UpdateView: ';



            const parent = this.GetParent ();

            // Guard Clause
            if (!parent)
                return;

            SilkFlo.Models.Abstract.GetModelFromParent (
                parent,
                [
                    'EditButton', 'ShowProductsButton', 'DeleteButton', 'NewButton', 'SubscribeButton'
                ],
                '',
                false);

            // Guard Clause
            if (!parent.ModelElements)
            {
                console.log ( `${logPrefix}ModelElements missing` );
                return;
            }

            // Get the elements
            const elements = parent.ModelElements;


            // Do the business
            if (this.SelectedRow)
            {
                // Is selected
                if (elements.EditButton)
                    elements.EditButton.classList.remove ( 'hide' ); // Show Edit

                if (elements.DeleteButton)
                    elements.DeleteButton.classList.remove('hide'); // Show Delete

                if (elements.SubscribeButton)
                {
                    if (this.SelectedRow.hasAttribute ( 'SilkFlo-Status' ))
                    {
                        const status = this.SelectedRow.getAttribute('SilkFlo-Status');
                        if (status === 'Subscribed' || status === 'FreeTrial' || status === 'PaymentRequired')
                        {
                            elements.SubscribeButton.innerHTML = 'Resubscribe ...';
                            elements.SubscribeButton.classList.remove('hide'); // Hide Subscribe
                        }
                        else if (status === 'NoSubscription')
                        {
                            elements.SubscribeButton.innerHTML = 'Add Subscription ...';
                            elements.SubscribeButton.classList.remove('hide'); // Hide Subscribe
                        }
                        else if (status === 'Demo')
                            elements.SubscribeButton.classList.add ( 'hide' ); // Hide Subscribe
                    }
                    else
                    {
                        elements.SubscribeButton.classList.add('hide'); // Hide Subscribe
                    }
                }
            }
            else
            {
                // nothing selected
                if (elements.EditButton)
                    elements.EditButton.classList.add ( 'hide' ); // Hide Edit

                if (elements.DeleteButton)
                    elements.DeleteButton.classList.add('hide'); // Hide Delete

                if (elements.SubscribeButton)
                    elements.SubscribeButton.classList.add('hide'); // Hide Subscribe
            }
        },


        // SilkFlo.ViewModels.Settings.Tenants.Summary.SelectRow_Click
        SelectRow_Click: function (element)
        {
            // Guard Clause
            if (!element)
            {
                const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Summary.SelectRow_Click: ';
                console.log ( `${logPrefix}element parameter missing` );
                return;
            }

            this.SelectedRow = element;


            const parent = element.parentElement;

            const children = parent.children;

            const length = children.length;
            for (let i = 0; i < length; i++)
            {
                const child = children[i];
                child.classList.remove ( 'select' );
            }

            element.classList.add ( 'select' );

            this.UpdateView();
        },

        // SilkFlo.ViewModels.Settings.Tenants.Summary.Row_DblClick
        Row_DblClick: function (id)
        {
            // Guard Clause
            if (!id)
            {
                const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Summary.Row_DblClick: ';
                console.log ( `${logPrefix}id parameter missing` );
                return;
            }

            SilkFlo.ViewModels.Settings.Tenants.PopulateModal ( id );
        },

        // SilkFlo.ViewModels.Settings.Tenants.Summary.Edit_OnClick
        Edit_OnClick: function ()
        {
            // Guard Clause
            if (!this.SelectedRow)
            {
                const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Summary.Edit_OnClick: ';
                console.log ( `${logPrefix}this.SelectedRow missing` );
                return;
            }

            const id = this.SelectedRow.id;

            SilkFlo.ViewModels.Settings.Tenants.PopulateModal ( id );
        },

        // SilkFlo.ViewModels.Settings.Tenants.Summary.New_OnClick
        New_OnClick: function ()
        {
            SilkFlo.ViewModels.Settings.Tenants.PopulateModal ();
        },


        // SilkFlo.ViewModels.Settings.Tenants.Summary.Products_OnClick
        Products_OnClick: function ()
        {
            const modalId = '#modalViewProducts';
            $ ( modalId )
                .modal ( 'show' );
        },

        // SilkFlo.ViewModels.Settings.Tenants.Summary.Subscription_OnClick
        Subscription_OnClick: function ()
        {
            alert ( 'ToDo: Subscription' );
        },

        // SilkFlo.ViewModels.Settings.Tenants.Summary.Delete_OnClick
        Delete_OnClick: function ()
        {
            // Guard Clause
            if (!this.SelectedRow)
            {
                const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Summary.Delete_OnClick: ';
                console.log ( `${logPrefix}this.SelectedRow missing` );
                return;
            }

            SilkFlo.ViewModels.Settings.Tenants.Summary.IsDeleting = true;

            SilkFlo.Models.Business.Client.Delete (
                SilkFlo.ViewModels.Settings.Tenants.Summary.Delete_Callback,
                SilkFlo.ViewModels.Settings.Tenants.Summary.DeleteFailed_Callback,
                this.SelectedRow.id,
                'Tenant' );
        },


        // SilkFlo.ViewModels.Settings.Tenants.Summary.Delete_Callback
        Delete_Callback: function ()
        {
            console.log ( 'Delete_Callback' );
            SilkFlo.ViewModels.Settings.Tenants.Summary.IsDeleting = false;

            // Remove Row
        },


        // SilkFlo.ViewModels.Settings.Tenants.Summary.DeleteFailed_Callback
        DeleteFailed_Callback: function (
                                   feedback)
        {
            SilkFlo.ViewModels.Settings.Tenants.Summary.IsDeleting = false;

            const parent = SilkFlo.ViewModels.Settings.Tenants.Summary.GetParent ();

            // Give feedback
            SilkFlo.DataAccess.Feedback (
                feedback,
                parent.id );
        }
    },


    // SilkFlo.ViewModels.Settings.Tenants.GetParent
    GetParent: function ()
    {
        const id = 'Modal.Business.Client';


        const parent = document.getElementById ( id );


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.GetParent: ';
            console.log ( `${logPrefix}Element with id "${id}" missing` );
            return null;
        }

        return parent;
    },

    // SilkFlo.ViewModels.Settings.Tenants.ShowMessage
    ShowMessage: function (innerHtml)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.ShowMessage: ';

        const id = 'modalManageTenant';
        const parent = document.getElementById ( id );

        // Guard Clause
        if (!parent)
        {
            console.log ( `${logPrefix}Element with id ${id} missing` );
            return;
        }


        const name = 'Message';
        const element = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!element)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }


        element.innerHTML = innerHtml;
    },

    Search: function (
                searchText,
                page,
                targetElementId,
                insertFirstId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Search: ';

        if (!searchText)
        {
            const element = document.getElementById ( 'Settings.Tenants.Search' );

            //Guard Clause
            if (!element)
            {
                console.log ( logPrefix + 'Element with id "Settings.Tenants.Search" missing' );
                return;
            }

            searchText = element.value;
        }

        let isPage = true;
        if (!page || page === '1')
        {
            isPage = false;
        }


        let url = '/api/Settings/Tenants/Table';
        if (searchText)
        {
            url += `/Search/${searchText}`;
        }

        if (isPage)
        {
            url += `/page/${page}`;


            const selectedPageElement = document.getElementById ( 'Settings.Tenants.Table.SelectedPage' );

            //Guard Clause
            if (!selectedPageElement)
            {
                console.log ( logPrefix + 'Element with id "Settings.Tenants.Table.SelectedPage" missing' );
                return;
            }

            selectedPageElement.value = page;
        }

        if (insertFirstId)
        {
            url += `/FirstId/${insertFirstId}`;
        }

        SilkFlo.DataAccess.UpdateElementById (
            url,
            null,
            targetElementId );
    },


    // SilkFlo.ViewModels.Settings.Tenants.PopulateModal
    PopulateModal: function (clientId)
    {
        let url = '/api/business/tenants/GetModal';
        if (clientId)
            url += `/id/${clientId}`;

        const id = 'modalManageTenantLabel';
        const label = document.getElementById ( id );

        // Guard Clause
        if (!label)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.PopulateModal: ';
            console.log ( `${logPrefix}element with id "${id}" missing` );
            return;
        }


        const parent = SilkFlo.ViewModels.Settings.Tenants.GetParent ();
        if (!parent)
            return;


        SilkFlo.DataAccess.UpdateElement (
            url,
            null,
            parent,
            null,
            'GET',
            SilkFlo.ViewModels.Settings.Tenants.PopulateModalCallBack,
            '',
            '',
            SilkFlo.DataAccess.Feedback,
            'Settings.Tenants' );
    },


    // SilkFlo.ViewModels.Settings.Tenants.PopulateModalCallBack
    PopulateModalCallBack: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.PopulateModalCallBack: ';
        const id = 'modalManageTenant';
        const modal = document.getElementById ( id );
        if (!modal)
        {
            console.log ( `${logPrefix}Element with id "${id}" missing` );
            return;
        }


        const modalId = '#modalManageTenant';
        $ ( modalId )
            .modal ( 'show' );


        modal.addEventListener (
            'shown.bs.modal',
            SilkFlo.ViewModels.Settings.Tenants.SetFocusOnName );

        const parent = SilkFlo.ViewModels.Settings.Tenants.GetParent ();
        if (!parent)
            return;


        let name = 'Modal.Business.Client.Status';
        const element = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!element)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }


        name = 'Modal.Business.Client.Subscription_Container';
        const subscriptionContainer = parent.querySelector(`[name="${name}"]`);

        if (subscriptionContainer)
        {
            name = 'Modal.Business.Client.DateStart';
            const dateStart = parent.querySelector ( `[name="${name}"]` );

            // Guard Clause
            if (!dateStart)
            {
                console.log ( `${logPrefix}Element with name ${name} missing` );
                return;
            }

            name = 'Modal.Business.Client.DateEnd';
            const dateEnd = parent.querySelector ( `[name="${name}"]` );

            // Guard Clause
            if (!dateEnd)
            {
                console.log ( `${logPrefix}Element with name ${name} missing` );
                return;
            }


            name = 'Modal.Business.Client.DateButton';
            const btnDate = parent.querySelector ( `[name="${name}"]` );

            // Guard Clause
            if (!btnDate)
            {
                console.log ( `${logPrefix}Element with name ${name} missing` );
                return;
            }

            Delaney.UI.DatePicker.MainRangeSelector (
                dateStart,
                dateEnd,
                btnDate );
        }
    },


    // SilkFlo.ViewModels.Settings.Tenants.SetFocusOnName
    SetFocusOnName: function ()
    {
        const parent = SilkFlo.ViewModels.Settings.Tenants.GetParent ();
        if (!parent)
            return;

        const name = 'Modal.Business.Client.Name';
        const element = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.SetFocusOnName: ';
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        element.focus ();
    },


    // SilkFlo.ViewModels.Settings.Tenants.Save_OnClick
    Save_OnClick: function ()
    {
        SilkFlo.ViewModels.Settings.Tenants.SaveModal ( true );
    },

    // SilkFlo.ViewModels.Settings.Tenants.ShowInviteMessage
    ShowInviteMessage: function ()
    {
        const id = 'SendInviteMessage';
        const element = document.getElementById ( id );

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.Business.Idea.Summary.ShowInviteMessage: ';
            console.log ( `${logPrefix}Element with id ${id} missing` );
            return;
        }


        element.classList.add ( 'show' );
        element.style.display = 'block';
    },


    SendInvite_Yes: function ()
    {
        // SilkFlo.ViewModels.Settings.Tenants.SendInvite_Yes
        SilkFlo.ViewModels.Settings.Tenants.SaveModal (
            false,
            true );
    },

    SendInvite_No: function ()
    {
        // SilkFlo.ViewModels.Settings.Tenants.SendInvite_No
        SilkFlo.ViewModels.Settings.Tenants.SaveModal (
            false,
            false );
    },

    SaveModal: function (
                   validateOnly = false,
                   sendInvite = false)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.SaveModal: ';

        const parent = SilkFlo.ViewModels.Settings.Tenants.GetParent ();
        if (!parent)
            return;


        const model = SilkFlo.Models.Business.Client.GetModelFromParent (
            parent,
            [
                'Id',
                'Name',
                'IndustryId',
                'Address1',
                'Address2',
                'City',
                'State',
                'PostCode',
                'CountryId',
                'AverageWorkingDay',
                'AverageWorkingHour',
                'Website',
                'AccountOwnerFirstName',
                'AccountOwnerLastName',
                'AccountOwnerEmail',
                'AccountOwnerId',
                'ProductId',
                'TypeId',
                'DateStart',
                'DateEnd'
            ],
            parent.id + '.',
            false);

        if (!model.AverageWorkingDay)
            model.AverageWorkingDay = 0;

        if (!model.AverageWorkingHour)
            model.AverageWorkingHour = 0;


        let name = 'Business.Client.IsActive';
        const elementIsActive = parent.querySelector ( `[name="${name}"]` );

        if (elementIsActive)
        {
            if (elementIsActive.checked)
                model.IsActive = true;
            else
                model.IsActive = false;
        }


        name = 'Business.Client.TypeId';
        const elementTypeId = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (elementTypeId)
            model.TypeId = elementTypeId.value;


        name = 'Business.Client.IsDemo';
        const elementIsDemo = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (elementIsDemo)
        {
            let value = false;
            if (elementIsDemo.checked)
                value = true;

            model.IsDemo = value;
        }


        name = 'Business.Client.ReferrerDiscountId';
        const elementReferrerDiscountId = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (elementReferrerDiscountId)
            model.ReferrerDiscountId = elementReferrerDiscountId.value;


        name = 'Business.Client.ResellerDiscountId';
        const elementResellerDiscountId = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (elementResellerDiscountId)
            model.ResellerDiscountId = elementResellerDiscountId.value;


        // Guard Clause
        if (!model)
        {
            console.log ( logPrefix + 'User model missing' );
            return;
        }


        const id = 'modalManageTenant';
        const parentModal = document.getElementById ( id );

        // Guard Clause
        if (!parentModal)
        {
            console.log ( `${logPrefix}Element with id ${id} missing` );
            return;
        }


        name = 'Modal.Business.Client.SaveButton';
        let element = parentModal.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!element)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        element.setAttribute (
            'disabled',
            '' );
        element.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" style="margin-right: 1rem;"></span>Saving...';


        if (model.AccountOwnerEmail && !model.Id)
        {
            name = 'Message';
            element = parentModal.querySelector ( `[name="${name}"]` );

            // Guard Clause
            if (!element)
            {
                console.log ( `${logPrefix}Element with name ${name} missing` );
                return;
            }

            element.innerHTML = '<span class="text-info">Please wait while email address is validated.</span>';


            SilkFlo.Models2.Business.Client.Model = model;
        }

        let url = '/api/Models/Business/Tenant/SaveModal';


        if (!model.Id && validateOnly)
        {
            url += `/validateOnly/${validateOnly}`;

            SilkFlo.Models.Business.Client.Save (
                model,
                SilkFlo.ViewModels.Settings.Tenants.ValidateSuccess_Callback,
                SilkFlo.ViewModels.Settings.Tenants.SaveFailed_Callback,
                parent.id,
                url );

            return;
        }


        if (!model.Id && sendInvite)
            url += `/sendInvite/${sendInvite}`;


        SilkFlo.Models.Business.Client.Save (
            model,
            SilkFlo.ViewModels.Settings.Tenants.Save_Callback,
            SilkFlo.ViewModels.Settings.Tenants.SaveFailed_Callback,
            parent.id,
            url );
    },


    // SilkFlo.ViewModels.Settings.Tenants.ValidateSuccess_Callback
    ValidateSuccess_Callback: function ()
    {
        const logPrefix = 'SilkFlo.Business.Idea.Summary.ValidateSuccess_Callback: ';
        const modalId = 'modalManageTenant';
        const parentModal = document.getElementById ( modalId );

        // Guard Clause
        if (!parentModal)
        {
            console.log ( `${logPrefix}Element with id ${id} missing` );
            return;
        }


        const name = 'Message';
        const elementMessage = parentModal.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!elementMessage)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        elementMessage.innerHTML = '';

        SilkFlo.ViewModels.Settings.Tenants.ShowInviteMessage ();
    },


    Save_Callback: function (id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.Save_Callback: ';


        const modalId = 'modalManageTenant';
        const parentModal = document.getElementById ( modalId );

        // Guard Clause
        if (!parentModal)
        {
            console.log ( `${logPrefix}Element with id ${id} missing` );
            return;
        }


        let name = 'Modal.Business.Client.SaveButton';
        const elementButton = parentModal.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!elementButton)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        elementButton.removeAttribute ( 'disabled' );
        elementButton.innerHTML = 'Save';


        name = 'Message';
        const elementMessage = parentModal.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!elementMessage)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        elementMessage.innerHTML = '';


        const model = SilkFlo.Models2.Business.Client.Model;

        if (model && model.Id)
        {
            // Update table
            const row = document.getElementById ( model.Id );

            // Guard Clause
            if (!row)
            {
                console.log ( logPrefix + 'tr element with id ' + model.Id + ' missing' );
                return;
            }

            SilkFlo.DataAccess.UpdateTargetElementWithURL (
                row,
                `/api/Settings/Tenant/TableRow/${model.Id}` );
        }
        else
        {
            // Reload table
            let elementId = 'Settings.Tenants.Search';
            const searchElement = document.getElementById ( elementId );

            // Guard Clause
            if (!searchElement)
            {
                console.log ( `${logPrefix}tr element with id "${elementId}" missing` );
                return;
            }

            const searchText = searchElement.value;


            elementId = 'Settings.Tenants.Table.SelectedPage';
            const pageElement = document.getElementById ( elementId );

            let page = '';
            if (pageElement)
            {
                page = pageElement.value;
            }


            const targetElementId = 'Settings.Tenants.Table';
            SilkFlo.ViewModels.Settings.Tenants.Search (
                searchText,
                page,
                targetElementId,
                id );
        }
        SilkFlo.SideBar.SetWorkspace ();

        window.$ ( '#modalManageTenant' )
            .modal ( 'hide' );
    },

    SaveFailed_Callback: function (
                             feedback,
                             id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.Tenants.SaveFailed_Callback: ';


        const modalId = 'modalManageTenant';
        const parentModal = document.getElementById ( modalId );

        // Guard Clause
        if (!parentModal)
        {
            console.log ( `${logPrefix}Element with id ${id} missing` );
            return;
        }


        const name = 'Modal.Business.Client.SaveButton';
        const element = parentModal.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!element)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }

        element.removeAttribute ( 'disabled' );
        element.innerHTML = 'Save';

        SilkFlo.ViewModels.Settings.Tenants.ShowMessage ( '' );


        // Give feedback
        SilkFlo.DataAccess.Feedback (
            feedback,
            id );
    },


    // SilkFlo.ViewModels.Settings.Tenants.SendInviteOnClick
    SendInviteOnClick: function (userId)
    {
        SilkFlo.ViewModels.Settings.Tenants.SendInvite ( userId );
    },


    // SilkFlo.ViewModels.Settings.Tenants.SendInvite
    SendInvite: function (userId)
    {
        const parent = SilkFlo.ViewModels.Settings.Tenants.GetParent ();
        if (!parent)
            return;


        const url = `/api/Business/Tenant/SendInvite/UserId/${userId}`;

        const http = new XMLHttpRequest ();
        http.open (
            'GET',
            url,
            true ); // false for synchronous request


        http.setRequestHeader (
            'Content-type',
            'application/x-www-form-urlencoded' );

        SilkFlo.ViewModels.Settings.Tenants.ShowMessage ( '' );

        http.onreadystatechange = function ()
        {
            return function ()
            {
                if (http.readyState === XMLHttpRequest.DONE && http.status === 200)
                {
                    const str = http.responseText;

                    SilkFlo.ViewModels.Settings.Tenants.ShowMessage ( str );
                }
            };
        } ( this );


        http.send ();
    }
};