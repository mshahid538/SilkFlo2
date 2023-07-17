if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};

if (!SilkFlo.ViewModels.Settings.PlatformSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup = {};

if (!SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup = {};

// CostSetup.InitialCosts Namespace: Code to manage the content inside the CostSetup > InitialCosts tab
SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts = {

    IsNew: null,
    SelectedRow: null,
    EditRow: null,
    IsLoading: false,


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.GetParent
    GetParent: function ()
    {
        const id = 'Settings.PlatformSetup.PlatformSetup.CostSetup.InitialCosts.Container';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.SetMessage
    SetMessage: function (text, cls)
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.GetParent();

        if (!parent)
            return;

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.SetMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.setAttribute('class', cls);
        element.innerHTML = text;
    },

    UpdateNewButtonToolTip: function (text)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.UpdateNewButtonToolTip: ';

        if (!text)
            return;


        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;



        const elementName = 'NewButton';
        const newButton = parent.querySelector(`[name="${elementName}"]`);

        // Guard Clause
        if (!newButton)
        {
            console.log(`${logPrefix}Element with name ${elementName} missing`);
            return;
        }


        this.NewToolTip = text;
        this.NewButtonElement = newButton;

        newButton.onmousemove = this.ShowNewToolTip;
        newButton.onmouseout = Delaney.UI.ToolTip.Hide;
    },

    UpdateEditButtonToolTip: function (text)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.UpdateEditButtonToolTip: ';

        if (!text)
            return;


        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;



        const elementName = 'EditButton';
        const editButton = parent.querySelector(`[name="${elementName}"]`);

        // Guard Clause
        if (!editButton)
        {
            console.log(`${logPrefix}Element with name ${elementName} missing`);
            return;
        }


        this.EditToolTip = text;
        this.EditButtonElement = editButton;

        editButton.onmousemove = this.ShowEditToolTip;
        editButton.onmouseout = Delaney.UI.ToolTip.Hide;
    },

    ShowEditToolTip: function ()
    {
        const toolTip = this.EditToolTip;
        const element = this.EditButtonElement;
        Delaney.UI.ToolTip.Show(event, toolTip, element);
    },

    ShowNewToolTip: function ()
    {
        const toolTip = this.NewToolTip;
        const element = this.NewButtonElement;
        Delaney.UI.ToolTip.Show(event, toolTip, element);
    },

    UpdateView: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.UpdateView: ';

        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;


        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['NewButton', 'EditButton', 'DeleteButton', 'SaveButton', 'CancelButton'],
            '');


        // Guard Clause
        if (!parent.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }


        // Get the elements
        const elements = parent.ModelElements;

        if (this.IsLoading)
        {
            elements.NewButton.classList.add('hide');
            elements.EditButton.classList.add('hide');      // Hide Edit
            elements.CancelButton.classList.add('hide');    // Hide Cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
            elements.DeleteButton.classList.add('hide');    // Hide Delete
            return;
        }

        // Do the business
        if (this.IsNew
            || this.EditRow)
            elements.NewButton.classList.add('hide');
        else
            elements.NewButton.classList.remove('hide');


        if (this.SelectedRow)
        {
            // Is selected
            elements.EditButton.classList.remove('hide');    // Show Edit
            elements.CancelButton.classList.add('hide');     // Hide Cancel
            elements.SaveButton.classList.add('hide');       // Hide Save
            elements.DeleteButton.classList.remove('hide');  // Show Delete
        }
        else if (this.EditRow)
        {
            // Is edit
            elements.EditButton.classList.add('hide');      // Hide edit
            elements.CancelButton.classList.remove('hide'); // Show cancel
            elements.SaveButton.classList.remove('hide');   // Show save
            elements.DeleteButton.classList.add('hide');    // Hide delete
        } else
        {
            // nothing selected
            elements.EditButton.classList.add('hide');      // Hide Edit
            elements.CancelButton.classList.add('hide');    // Hide cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
            elements.DeleteButton.classList.add('hide');    // Hide Delete
        }
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.New_Click
    New_Click: function ()
    {
        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;

        this.Cancel_Click();

        this.IsLoading = true;
        this.UpdateView();

        const url = '/api/settings/Tenant/platformSetup/CostSetup/InitialCosts/NewRow';
        const tbody = parent.querySelector('tbody');
        SilkFlo.DataAccess.AppendElement(
            url,
            null,
            tbody,
            this.New_CallBack,
            true);
    },

    New_CallBack: function ()
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const tbody = parent.querySelector('tbody');

        const row = tbody.children[0];
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.IsLoading = false;

        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.SelectRow_Click(row);
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Edit_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.IsNew = true;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.UpdateView();
    },

    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Cancel_Click
    Cancel_Click: function ()
    {
        // Guard Clause
        if (!this.EditRow)
            return;

        if (this.EditRow.isEqualNode(this.SelectedRow))
            return;


        const row = this.EditRow;
        this.EditRow = null;
        this.SelectedRow = row;


        if (this.IsNew)
        {
            row.remove();
            this.SelectedRow = null;
            this.IsNew = false;
            this.UpdateView();
            return;
        }


        row.classList.remove('edit');


        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent (
            row,
            [
                'Name',
                'RoleId',
                'CostDisplay',
                'Cost',
                'MonthlyCostDisplay',
                'MonthlyCost',
                'AnnualCostDisplay',
                'AnnualCost'
            ],
            'Business.RoleCost.' );


        const elements = row.ModelElements;



        // Do the business
        const oldId = elements.Name.getAttribute('oldId');
        elements.RoleId.value = oldId;

        let old = elements.CostDisplay.getAttribute('old');
        elements.Cost.innerHTML = old;

        elements.MonthlyCost.innerHTML = elements.MonthlyCostDisplay.getAttribute('old');
        elements.AnnualCost.innerHTML = elements.AnnualCostDisplay.getAttribute ( 'old' );

        // Deselect
        if (window.getSelection)
            window.getSelection().removeAllRanges();

        else if (document.selection)
            document.selection.empty();


        this.UpdateView();
    },

    SelectRow_Click: function (element)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.SelectRow_Click: ';
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        if (this.EditRow
            && element.isEqualNode(this.EditRow))
            return;

        this.SelectedRow = element;

        this.Cancel_Click();


        const parent = element.parentElement;

        const children = parent.children;

        const length = children.length;
        for (let i = 0; i < length; i++)
        {
            const child = children[i];
            child.classList.remove('select');
        }

        element.classList.add('select');

        this.UpdateView();
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Edit_Click
    Edit_Click: function ()
    {
        this.Cancel_Click();

        const row = this.SelectedRow;
        this.EditRow = row;
        this.SelectedRow = null;

        // Guard Clause
        if (!row)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Edit_Click: ';
            console.log(`${logPrefix}this.SelectedRow missing`);
            return;
        }


        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Cost',
                'MonthlyCost',
                'AnnualCost'
            ],
            'Business.RoleCost.');

       
        // Do the business
        row.classList.add('edit');
        this.IsNew = false;

        
        Delaney.UI.ToolTip.Hide();

        this.UpdateView();
    },



    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.KeyPress
    KeyPress: function (event)
    {
        if (event.which === 13)
            event.preventDefault();

        if (event.key === 'Enter')
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Save_Click();

        if (event.key === 'Escape')
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Cancel_Click();
    },

    Dbl_Click: function (element)
    {
        this.SelectRow_Click(element);
        this.Edit_Click();
    },

    Save_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Save_Click: ';

        if (!this.EditRow)
        {
            console.log(`${logPrefix}this.EditRow parameter missing`);
            return;
        }

        const row = this.EditRow;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'RoleId',
                'Cost'
            ],
            'Business.RoleCost.');


        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'Model is missing');
            return;
        }



        // Guard Clause
        if (!model.RoleId)
        {
            this.SetMessage('Please select a role', 'text-warning');
            return;
        }



        const url = '/api/Business/RoleCost/Post';
        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Save_CallBack,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.PlatformSetup.CostSetup.InitialCosts.Container',
            url,
            'POST');
    },


    Save_CallBack: function (
                       str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Save_CallBack: ';

        // Guard Clause
        if (!str)
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Cancel_Click();
            return;
        }


        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.EditRow;

        // Guard Clause
        if (!row)
        {
            console.log(`${logPrefix}this.EditRow missing`);
            return;
        }



        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'Name',
                'RoleId',
                'CostDisplay',
                'Cost',
                'Cost_Container',
                'MonthlyCostDisplay',
                'MonthlyCost',
                'MonthlyCost_Container',
                'AnnualCostDisplay',
                'AnnualCost',
                'AnnualCost_Container'
            ],
            'Business.RoleCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;




        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.GetParent();
        let name = 'Business.InitialCosts.Currency';
        const currencyElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!currencyElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }








        // Do the business
        elements.Name.setAttribute (
            'oldId',
            elements.RoleId.value);


        const selectedOption = elements.RoleId.options[elements.RoleId.selectedIndex];

        console.log(logPrefix + 'selectedOption.text = ' + selectedOption.text);

        elements.Name.innerHTML = selectedOption.text;
        elements.Name.setAttribute (
            'old',
            selectedOption.text );


        let symbol = currencyElement.value;

        let value = elements.Cost.innerHTML;
        elements.CostDisplay.setAttribute ('old', value);
        elements.CostDisplay.innerHTML = `${symbol}&nbsp;${value}`;


        value = elements.MonthlyCost.innerHTML;
        elements.MonthlyCostDisplay.setAttribute('old', value);
        elements.MonthlyCostDisplay.innerHTML = `${symbol}&nbsp;${value}`;


        value = elements.AnnualCost.innerHTML;
        elements.AnnualCostDisplay.setAttribute('old', value);
        elements.AnnualCostDisplay.innerHTML = `${symbol}&nbsp;${value}`;



        if (SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.IsNew)
        {
            // Guard Clause
            if (!elements.Id)
            {
                console.log(`${logPrefix}Element.Id missing`);
                return;
            }

            elements.Id.innerHTML = str;
        }




        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.IsNew = false;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Cancel_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.SetMessage(
            'Saved',
            'text-success');
    },




    Delete_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Delete_Click: ';

        const row = this.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.SelectedRow missing');
            return;
        }



        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'Name'
            ],
            'Business.RoleCost.');


        if (!model || !model.Id)
        {
            console.log(logPrefix + 'Cannot find software vender Id');
            return;
        }


        if (!model.Name)
        {
            console.log(logPrefix + 'Cannot find software vender');
            return;
        }



        const url = `/api/Business/RoleCost/Delete/${model.Id}`;


        SilkFlo.Models.Abstract.Delete(
            url,
            `${model.Name}`,
            '',
            this.Delete_Callback,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.PlatformSetup.CostSetup.InitialCosts.Container');
    },


    Delete_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Delete_Callback: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'this.SelectedRow missing');
            return;
        }


        row.classList.remove('select');
        row.classList.add('glow-border-red');

        setTimeout(function ()
        {
            row.classList.remove('glow-border-red');
            row.remove();
        }, 1000);

        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.SelectedRow = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.UpdateView();
    },




    Search: function (
                searchText,
                page,
                targetElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Search: ';


        // Guard Clause
        if (!targetElementId)
        {
            console.log(`${logPrefix}targetElementId parameter missing`);
            return;
        }


        const targetElement = document.getElementById(targetElementId);

        // Guard Clause
        if (!targetElement)
        {
            console.log(`${logPrefix}Element with id ${targetElementId} missing`);
            return;
        }


        if (!searchText)
        {
            const id = 'Settings.PlatformSetup.CostSetup.InitialCosts.Search';
            const element = document.getElementById(id);


            // Guard Clause
            if (!element)
            {
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }

            searchText = element.value;
        }


        let isPage = true;
        if ((page === undefined
                || page === null
                || page === '')
            || page === '1')
        {
            isPage = false;
        }

        let isSearchText = true;
        if (!searchText)
        {
            isSearchText = false;
        }

        let url = '/api/settings/Tenant/platformSetup/CostSetup/InitialCosts';
        if (isSearchText)
        {
            url += `/Search/${searchText}`;
        }

        if (isPage)
        {
            url += `/page/${page}`;
        }

        this.SelectedRow = null;

        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            targetElement,
            '',
            'GET',
            this.SearchResultCallBack);
    },




    Cost_OnInput: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.Cost_OnInput: ';

        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.EditRow;

        // Guard Clause
        if (!row)
            return;


        const name = 'Business.InitialCosts.AverageWorkingDay';
        const averageWorkingDay = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!averageWorkingDay)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'MonthlyCost',
                'AnnualCost'
            ],
            'Business.RoleCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        const value = event.target.innerHTML;

        let averageWorkingDayValue = averageWorkingDay.value;
        if (!averageWorkingDayValue || averageWorkingDayValue === '0')
        {
            averageWorkingDayValue = 260;
        }


        let annualCostValue = averageWorkingDayValue * value;
        let monthlyCostValue = annualCostValue / 12;

        annualCostValue = annualCostValue.toFixed(2);
        monthlyCostValue = monthlyCostValue.toFixed(2);

        elements.AnnualCost.innerHTML = annualCostValue;
        elements.MonthlyCost.innerHTML = monthlyCostValue;
    },

    MonthlyCost_OnInput: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.MonthlyCost_OnInput: ';

        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const name = 'Business.InitialCosts.AverageWorkingDay';
        const averageWorkingDay = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!averageWorkingDay)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.EditRow;

        // Guard Clause
        if (!row)
            return;

  
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Cost',
                'AnnualCost'
            ],
            'Business.RoleCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        const value = event.target.innerHTML;
        let averageWorkingDayValue = averageWorkingDay.value;
        if (!averageWorkingDayValue || averageWorkingDayValue === '0')
        {
            averageWorkingDayValue = 260;
        }

        let annualCostValue = value * 12;
        let costValue = annualCostValue / averageWorkingDayValue;

        annualCostValue = annualCostValue.toFixed(2);
        costValue = costValue.toFixed(2);

        elements.AnnualCost.innerHTML = annualCostValue;
        elements.Cost.innerHTML = costValue;
    },

    AnnualCost_OnInput: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.AnnualCost_OnInput: ';

        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        let name = 'Business.InitialCosts.AverageWorkingDay';
        const averageWorkingDay = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!averageWorkingDay)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.InitialCosts.EditRow;

        // Guard Clause
        if (!row)
            return;


        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Cost',
                'MonthlyCost'
            ],
            'Business.RoleCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        const value = event.target.innerHTML;
        let averageWorkingDayValue = averageWorkingDay.value;
        if (!averageWorkingDayValue || averageWorkingDayValue === '0')
        {
            averageWorkingDayValue = 260;
        }

        const costValue = value / averageWorkingDayValue;
        const monthlyCostValue = value / 12;

        elements.Cost.innerHTML = costValue.toFixed(2);
        elements.MonthlyCost.innerHTML = monthlyCostValue.toFixed (2);
    }
};