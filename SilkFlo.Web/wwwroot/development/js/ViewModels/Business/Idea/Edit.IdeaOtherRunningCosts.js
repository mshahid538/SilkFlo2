if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};

if (!SilkFlo.ViewModels.Business.Idea.Edit)
    SilkFlo.ViewModels.Business.Idea.Edit = {};

SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts = {

    ParentId: null,


    GetCurrency: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetCurrency: ';


        const id = 'Business.Idea.Currency';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        if (!element.value)
        {
            console.log(`${logPrefix}currency symbol missing`);
            return '';
        }

        return element.value;
    },



    GetParent: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.GetParent: ';
        const id = SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.ParentId;

        // Guard Clause
        if (!id)
        {
            console.log(`${logPrefix}ParentId is null`);
            return null;
        }


        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },


    UpdateView: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateView: ';

        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;


        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['NewButton', 'DeleteButton'],
            '');

        // Guard Clause
        if (!parent.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }


        // Get the elements
        const elements = parent.ModelElements;


        // Do the business

        // Is Loading
        if (parent.IsLoading)
        {
            elements.NewButton.classList.add ( 'hide' ); // Hide New
            elements.DeleteButton.classList.add ( 'hide' ); // Hide Delete
            return;
        }
        else
        {
            elements.NewButton.classList.remove('hide'); // Hide New

            // nothing selected
            if (parent.SelectedRow)
                elements.DeleteButton.classList.remove('hide');    // Hide Delete
            else
                elements.DeleteButton.classList.add('hide');    // Hide Delete
        }
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.New_Click
    New_Click: function (parentId, costTypeId)
    {
        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.ParentId = parentId;

        const parent = SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;


        parent.IsLoading = true;
        this.UpdateView();
        const url = '/api/Business/ImplementationCost/GetOtherRunningCosts/NewRow/' + costTypeId;
        const tbody = parent.querySelector('tbody');


        SilkFlo.DataAccess.AppendElement(
            url,
            null,
            tbody,
            this.New_CallBack,
            true,
            tbody.lastElementChild);
    },

    New_CallBack: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const tbody = parent.querySelector('tbody');

        let length = tbody.children.length;
        if (length)
            length -= 2;

        const row = tbody.children[length];
        parent.IsLoading = false;
        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.SelectRow_Click(row, parent.id);
        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateView();
    },


    SelectRow_Click: function (element, parentId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.SelectRow_Click: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        // Guard Clause
        if (!parentId)
        {
            console.log(`${logPrefix}parentId parameter missing`);
            return;
        }

        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.ParentId = parentId;

        if (this.EditRow
            && element.isEqualNode(this.EditRow))
            return;


        const parent = SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.GetParent();

        if (!parent)
            return;

        parent.SelectedRow = element;


        const tbody = element.parentElement;

        const children = tbody.children;

        const length = children.length;
        for (let i = 0; i < length; i++)
        {
            const child = children[i];
            child.classList.remove('select');
        }

        element.classList.add('select');

        this.UpdateView();
    },


    Delete_Click: function (parentId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.Delete_Click: ';


        // Guard Clause
        if (!parentId)
        {
            console.log(`${logPrefix}parentId parameter missing`);
            return;
        }


        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.ParentId = parentId;
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.GetParent();

        const row = parent.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'parent.SelectedRow missing');
            return;
        }


        const name = 'Business.IdeaOtherRunningCost.OtherRunningCostId';
        const element = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        let text = 'unknown';

        if (element.selectedIndex !== -1)
            text = element.children[element.selectedIndex].textContent;

        bootbox.dialog({
            title: `Delete ${name}`,
            message: `Are you sure that you want to delete <b>${text}</b>?`,
            onEscape: true,
            backdrop: true,
            buttons: {
                cancel: {
                    label: 'Cancel',
                    className: 'btn-secondary bootbox-accept'
                },
                delete: {
                    label: 'Delete',
                    className: 'btn-danger',
                    callback: SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.Delete_Callback

                }
            }
        });

    },

    Delete_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.Delete_Callback: ';

        const parent = SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.GetParent();


        const row = parent.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'parent.SelectedRow missing');
            return;
        }
        row.classList.remove('select');
        row.classList.add('glow-border-red');
        parent.SelectedRow = null;

        setTimeout(function ()
        {
            row.classList.remove('glow-border-red');
            row.remove();

            SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateView();

            const parent = SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.GetParent();

            const name = 'tbody';
            const tbody = parent.querySelector(`[name="${name}"]`);;

            // Guard Clause
            if (!tbody)
            {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateTotals(tbody);

        }, 1000);



    },

    OtherRunningCost_Change: function (element, parentId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.RunningCost_Change: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        // Guard Clause
        if (!parentId)
        {
            console.log(`${logPrefix}parentId parameter missing`);
            return;
        }

        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.ParentId = parentId;

        const row = element.parentElement.parentElement.parentElement;


        let description = '';
        if (element.selectedIndex !== -1)
        {
            const option = element.children[element.selectedIndex];
            description = option.getAttribute('description');
        }

        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            ['More', 'None', 'Description'],
            'Business.IdeaOtherRunningCost.');

        const moreElement = row.ModelElements.More;
        const noneElement = row.ModelElements.None;
        const descriptionElement = row.ModelElements.Description;
        descriptionElement.innerHTML = description;
        if (description)
        {
            moreElement.style.display = 'inline';
            noneElement.style.display = 'none';
        }
        else
        {
            moreElement.style.display = 'none';
            noneElement.style.display = 'inline';
        }




        if (row.localName !== 'tr')
        {
            console.log(logPrefix + 'parent not tr');
            return;
        }

        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateCosts(row);
    },

    Number_Input: function (element, parentId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.Number_Input: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        // Guard Clause
        if (!parentId)
        {
            console.log(`${logPrefix}parentId parameter missing`);
            return;
        }

        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.ParentId = parentId;

        const row = element.parentElement.parentElement;


        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateCosts ( row );
    },

    UpdateCosts: function (rowElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateCosts: ';

        // Guard Clause
        if (!rowElement)
        {
            console.log(`${logPrefix}rowElement parameter missing`);
            return;
        }


        // Guard Clause
        if (rowElement.localName !== 'tr')
        {
            console.log(`${logPrefix}rowElement is not a tr. It is a ${rowElement.localName}`);
            return;
        }


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            rowElement,
            ['OtherRunningCostId', 'Number', 'CostPerYear', 'CostPerMonth', 'TotalCostPerYear', 'TotalCostPerMonth'],
            'Business.IdeaOtherRunningCost.');


        let licenceCount = model.Number;

        if (!licenceCount)
            licenceCount = 0;



        let costYear = 0;
        let costMonth = 0;
        let totalCostYearValue = 0;
        let totalCostMonthValue = 0;

        const otherRunningCostIdElement = rowElement.ModelElements.OtherRunningCostId;

        let period = 'Annual';
        let cost = 0;
        let description = '';
        if (otherRunningCostIdElement.selectedIndex !== -1)
        {
            const option = otherRunningCostIdElement.children[otherRunningCostIdElement.selectedIndex];
            period = option.getAttribute('period');
            cost = option.getAttribute('cost');
            description = option.getAttribute('description');
        }


        if (period === 'Annual')
        {
            costYear = cost * 1;
            costMonth = cost / 12;
        }
        else
        {
            costYear = cost * 12;
            costMonth = cost * 1;
        }

        totalCostYearValue = costYear * licenceCount;
        totalCostMonthValue = costMonth * licenceCount;


        costYear = SilkFlo.FormatNumber(costYear);
        costMonth = SilkFlo.FormatNumber(costMonth);
        const totalCostYear = SilkFlo.FormatNumber(totalCostYearValue);
        const totalCostMonth = SilkFlo.FormatNumber(totalCostMonthValue);

        // Apply Values
        let currency = SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetCurrency();
        if (!currency)
            return;

        rowElement.ModelElements.CostPerYear.innerHTML = currency + '&nbsp;' + costYear;
        rowElement.ModelElements.CostPerMonth.innerHTML = currency + '&nbsp;' + costMonth;

        rowElement.ModelElements.TotalCostPerYear.innerHTML = currency + '&nbsp;' + totalCostYear;
        rowElement.ModelElements.TotalCostPerMonth.innerHTML = currency + '&nbsp;' + totalCostMonth;

        rowElement.ModelElements.TotalCostPerYear.setAttribute (
            'value',
            totalCostYearValue);

        rowElement.ModelElements.TotalCostPerMonth.setAttribute(
            'value',
            totalCostMonthValue);

        const tbody = rowElement.parentElement;


        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateTotals(tbody );
    },


    UpdateTotals: function (tbody)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateTotals: ';

        // Guard Clause
        if (!tbody)
        {
            console.log(`${logPrefix}tbody parameter missing`);
            return;
        }

        const models = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            tbody,
            'Business.IdeaOtherRunningCost',
            ['TotalCostPerYear', 'TotalCostPerMonth'],
            'Business.IdeaOtherRunningCost.');



        let totalCostYear = 0;
        let totalCostMonth = 0;

        const length = models.length;
        for (let i = 0; i < length; i++)
        {
            const row = tbody.ModelElements[i];
            const model = row.Model;
            const modelElements = row.ModelElements;

            const totalCostYearValue = modelElements.TotalCostPerYear.getAttribute('value');
            totalCostYear += totalCostYearValue * 1;

            const totalCostMonthValue = modelElements.TotalCostPerMonth.getAttribute('value');
            totalCostMonth += totalCostMonthValue * 1;
        }

        let name = 'TotalCostPerYear';
        const totalCostPerYearElement = tbody.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!totalCostPerYearElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = 'TotalCostPerMonth';
        const totalCostPerMonthElement = tbody.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!totalCostPerMonthElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }



        let currency = SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetCurrency();
        if (!currency)
            return;

        totalCostPerYearElement.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(totalCostYear);
        totalCostPerMonthElement.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(totalCostMonth);

        SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.UpdateTotals ();
    }
}