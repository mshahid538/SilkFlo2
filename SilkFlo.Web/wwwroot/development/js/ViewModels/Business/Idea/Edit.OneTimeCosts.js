if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};

if (!SilkFlo.ViewModels.Business.Idea.Edit)
    SilkFlo.ViewModels.Business.Idea.Edit = {};

SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts = {

    SelectedRow: null,
    IsLoading: false,
    ParentId: null,

    GetCurrency: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.GetCurrency: ';


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
        const id = 'Business.Idea.CostBenefit.CostEstimates.OneTimeCosts';

        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },


    UpdateView: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.UpdateView: ';

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
            elements.NewButton.classList.add('hide'); // Hide New
            elements.DeleteButton.classList.add('hide'); // Hide Delete
            return;
        }
        else
        {
            elements.NewButton.classList.remove('hide'); // Hide New

            // nothing selected
            if (this.SelectedRow)
                elements.DeleteButton.classList.remove('hide');    // Hide Delete
            else
                elements.DeleteButton.classList.add('hide');    // Hide Delete
        }
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.New_Click
    New_Click: function (ideaId)
    {
        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;


        this.IsLoading = true;
        this.UpdateView();
        const url = `/api/Business/ImplementationCost/GetOneTimeCosts/NewRow/IdeaId/${ideaId}`;
        const tbody = parent.querySelector('tbody');


        SilkFlo.DataAccess.AppendElement(
            url,
            null,
            tbody,
            this.New_CallBack);
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.New_CallBack
    New_CallBack: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const tbody = parent.querySelector('tbody');

        let length = tbody.children.length;
        if (length)
            length -= 1;

        const row = tbody.children[length];
        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.IsLoading = false;
        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.SelectRow_Click(row);
        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.UpdateView();
        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.UpdateCycleDays();
        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.CalculateRowTotal(row);
    },


    SelectRow_Click: function (element)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.SelectRow_Click: ';
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        if (this.EditRow
            && element.isEqualNode(this.EditRow))
            return;

        this.SelectedRow = element;


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


    Delete_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.Delete_Click: ';

        const row = this.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.SelectedRow missing');
            return;
        }



        let name = 'Business.ImplementationCost.IdeaStageId';
        const elementIdeaStage = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementIdeaStage)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = 'Business.ImplementationCost.RoleId';
        const elementRole = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementRole)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        let textStage = 'unknown stage';
        let textRole = 'unknown role';


        if (elementIdeaStage.selectedIndex !== -1)
            textStage = elementIdeaStage.children[elementIdeaStage.selectedIndex].textContent;

        if (elementRole.selectedIndex !== -1)
            textRole = elementRole.children[elementRole.selectedIndex].textContent;


        bootbox.dialog({
            title: `Delete ${name}`,
            message: `Are you sure that you want to delete <b>${textStage}&nbsp;-&nbsp;${textRole}</b>?`,
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
                    callback: SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.Delete_Callback

                }
            }
        });
    },

    Delete_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.Delete_Callback: ';

        const row = SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.SelectedRow missing');
            return;
        }
        row.classList.remove('select');
        row.classList.add('glow-border-red');
        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.SelectedRow = null;

        setTimeout(function ()
        {
            row.remove();

            SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.UpdateView();

            SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.CalculateTotals ();
        }, 1000);
    },

    //SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.Stage_Change
    IdeaStage_Change: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.IdeaStage_Change: ';


        const tr = element.parentElement.parentElement.parentElement;

        const name = 'CycleDays';
        const cycleDays = tr.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!cycleDays)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        cycleDays.children[0].setAttribute('name',`CycleDays${element.value}`);

        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.CalculateRowTotal(tr);
        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.UpdateCycleDays();
    },

    Role_Change: function (element)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.Role_Change: ';
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        const row = element.parentElement.parentElement;

        if (row.localName !== 'tr')
        {
            console.log(`${logPrefix}rowElement is not a tr. It is a ${rowElement.localName}`);
            return;
        }

        let description = '';
        if (element.selectedIndex !== -1)
        {
            const option = element.children[element.selectedIndex];
            description = option.getAttribute('description');
        }

        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            ['More', 'None', 'Description'],
            'Business.ImplementationCost.');

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

        this.CalculateRowTotal(row);
    },

    Allocation_Input: function (element)
    {

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.Allocation_Input: ';
            console.log(`${logPrefix}element parameter missing`);
            return;
        }


        this.CalculateRowTotal(element.parentElement.parentElement.parentElement);
    },

    Day_Input: function (element)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.Day_Input: ';
            console.log(`${logPrefix}element parameter missing`);
            return;
        }


        this.CalculateRowTotal(element.parentElement.parentElement);

    },

    CalculateRowTotal: function (rowElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.CalculateRowTotal: ';
        // Guard Clause
        if (!rowElement)
        {
            console.log(`${logPrefix}element parameter missing`);
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
            ['RoleId', 'Allocation', 'Day', 'CostDay', 'TotalCost', 'TotalCostValue'],
            'Business.ImplementationCost.');



        let cost = 0;
        const roleElement = rowElement.ModelElements.RoleId;
        if (roleElement.selectedIndex !== -1)
        {
            const option = roleElement.children[roleElement.selectedIndex];
            cost = option.getAttribute ( 'cost' );
        }

        const currency = SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.GetCurrency ();

        rowElement.ModelElements.CostDay.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(cost);

        //Total Cost = DayCost * Day /100 * AllocationPercent;
        const totalCost = cost * model.Day / 100 * model.Allocation;


        rowElement.ModelElements.TotalCost.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(totalCost);
        rowElement.ModelElements.TotalCostValue.value = totalCost;

        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.CalculateTotals ();
    },

    CalculateTotals: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.CalculateTotals: ';

        const parent = this.GetParent();

        if (!parent)
            return;


        let name = 'Business.ImplementationCosts';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        const models = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            element,
            'Business.ImplementationCost',
            ['IdeaStageId', 'TotalCostValue'],
            'Business.ImplementationCost.');


        let analysisTotal = 0;
        let solutionDesignTotal = 0;
        let developmentTotal = 0;
        let testingTotal = 0;
        let total = 0;

        const length = models.length;
        for (let i = 0; i < length; i++)
        {
            const model = models[i];
            const modelElement = element.ModelElements[i];

            const ideaStageElement = modelElement.ModelElements.IdeaStageId;


            //const stageId = optionElement.getAttribute('stageId');
            if (ideaStageElement.selectedIndex !== -1)
            {
                const optionElement = ideaStageElement.children[ideaStageElement.selectedIndex];
                const stageId = optionElement.getAttribute('stageId');

                if (stageId === 'n03_Analysis')
                    analysisTotal += model.TotalCostValue * 1;
                else if (stageId === 'n04_SolutionDesign')
                    solutionDesignTotal += model.TotalCostValue * 1;
                else if (stageId === 'n05_Development')
                    developmentTotal += model.TotalCostValue * 1;
                else if (stageId === 'n06_Testing')
                    testingTotal += model.TotalCostValue * 1;

                total += model.TotalCostValue * 1;
            }
        }



        name = `OneTimeCostn03_AnalysisTotal`;
        const elementAnalysisTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementAnalysisTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = `OneTimeCostn03_AnalysisTotalvalue`;
        const elementAnalysisTotalValue = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementAnalysisTotalValue)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = `OneTimeCostn04_SolutionDesignTotal`;
        const elementSolutionDesignTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementSolutionDesignTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = `OneTimeCostn04_SolutionDesignTotalvalue`;
        const elementSolutionDesignTotalValue = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementSolutionDesignTotalValue)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = `OneTimeCostn05_DevelopmentTotal`;
        const elementDevelopmentTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementDevelopmentTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = `OneTimeCostn05_DevelopmentTotalvalue`;
        const elementStageDevelopmentTotalValue = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementStageDevelopmentTotalValue)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }



        name = `OneTimeCostn06_TestingTotal`;
        const elementTestingTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementTestingTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = `OneTimeCostn06_TestingTotalvalue`;
        const elementStageTestingTotalValue = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementStageTestingTotalValue)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'Business.ImplementationCost.Total';
        const elementTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        const currency = SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.GetCurrency();


        elementAnalysisTotal.innerHTML = `${currency}\xA0${SilkFlo.FormatNumber(analysisTotal)}`;
        elementAnalysisTotalValue.value = analysisTotal;
        elementSolutionDesignTotal.innerHTML = `${currency}\xA0${SilkFlo.FormatNumber(solutionDesignTotal)}`;
        elementSolutionDesignTotalValue.value = solutionDesignTotal;
        elementDevelopmentTotal.innerHTML = `${currency}\xA0${SilkFlo.FormatNumber(developmentTotal)}`;
        elementStageDevelopmentTotalValue.value = developmentTotal;
        elementTestingTotal.innerHTML = `${currency}\xA0${SilkFlo.FormatNumber(testingTotal)}`;
        elementStageTestingTotalValue.value = testingTotal;

        elementTotal.innerHTML = `${currency}\xA0${SilkFlo.FormatNumber(total)}`;

        SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.UpdateTotals ();
    },

    UpdateCycleDays: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.UpdateCycleDays: ';

        let id = 'gantt1';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        id = 'LastNameCell';
        const elementLastNameCell = document.getElementById(id);



        let models = null;


        models = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            element,
            'Business.IdeaStage',
            [
                'Id', 'DateStart', 'DateEnd'
            ],
            'Business.IdeaStage.');


        const length = models.length;
        for (let i = 0; i < length; i++)
        {

            const model = models[i];

            if (!model.DateEnd)
                continue;

            let days = 0;



            const dateStart = Delaney.UI.DatePicker.Moments.GetDate(model.DateStart);

            const dateEnd = Delaney.UI.DatePicker.Moments.GetDate(model.DateEnd);

            
            // Get the days
            days = Delaney.UI.DatePicker.Moments.CountCertainDays (
                [
                    1, 2, 3, 4, 5
                ],
                dateStart,
                dateEnd );


            const name = `CycleDays${model.Id}`;
            const cycleDaysElements = document.querySelectorAll ( `[name="${name}"]` );

            // Guard Clause
            if (!cycleDaysElements)
            {
                console.log ( `${logPrefix}Element with name ${name} missing` );
                return;
            }


            const length2 = cycleDaysElements.length;
            for (let j = 0; j < length2; j++)
            {
                const cycleDaysElement = cycleDaysElements[j];
                console.log(cycleDaysElement);

                cycleDaysElement.innerHTML = days;
            }
        }
    }
}