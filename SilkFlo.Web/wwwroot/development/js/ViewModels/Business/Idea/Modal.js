// Add the model
if (SilkFlo.Business === undefined)
    SilkFlo.Business = {};

if (SilkFlo.Business.Idea === undefined)
    SilkFlo.Business.Idea = {};


SilkFlo.ViewModels.Business.Idea.Modal = {

    PageNumber: 0,
    PageCount: 10,
    Width: 798,
    ModalSubmitIdeaPages: null,
    CtrlErrors: null,
    IsValid: true,
    ValidateDepartments: false,

    // SilkFlo.ViewModels.Business.Idea.Modal.GetParent
    GetParent: function ()
    {
        const id = 'FormModal_BusinessIdea';
        const parent = document.getElementById(id);

        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Modal.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },

    // SilkFlo.ViewModels.Business.Idea.Modal.SetMessage
    SetMessage: function (text)
    {
        const parent = this.GetParent();
        if (!parent)
            return;

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Modal.SetMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = text;
    },


    // SilkFlo.ViewModels.Business.Idea.Modal.Reset
    Reset: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Modal.Reset: ';


        const id = 'ModalSubmitIdeaContent';
        let element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        element.style.paddingTop = '';
        element.style.paddingLeft = '';


        SilkFlo.ViewModels.Business.Idea.Modal.PageNumber = 0;
        SilkFlo.ViewModels.Business.Idea.Modal.ModalSubmitIdeaPages = null;

        const parentElementId = 'FormModal_BusinessIdea';
        const parentElement = document.getElementById(parentElementId);
        if (!parentElement)
        {
            console.log(`${logPrefix}Element with id "${parentElementId}" missing`);
            return;
        }


        let name = 'Business.Idea.RuleId';
        const rule = SilkFlo.GetElementInParent(parentElement, name);
        if (rule)
        {
            DelaneysSlider_Main(rule);
        }
        else
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
        }


        name = 'Business.Idea.InputId';
        const input = SilkFlo.GetElementInParent(parentElement, name);
        if (input)
        {
            DelaneysSlider_Main(input);
        }
        else
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
        }


        name = 'Business.Idea.InputDataStructureId';
        const inputDataStructure = SilkFlo.GetElementInParent(parentElement, name);
        if (inputDataStructure)
        {
            DelaneysSlider_Main(inputDataStructure);
        }
        else
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
        }

        name = 'Business.Idea.Rating';
        const rate = SilkFlo.GetElementInParent(parentElement, name);
        if (rate)
        {
            DelaneysSlider_Main(rate);
        }
        else
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
        }


        name = 'Business.Idea.DocumentationPresentId';
        const documentationPresent = SilkFlo.GetElementInParent(parentElement, name);
        if (documentationPresent)
        {
            DelaneysSlider_Main(documentationPresent);
        }
        else
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
        }

        name = 'Business.Idea.ProcessStabilityId';
        const processStability = SilkFlo.GetElementInParent(parentElement, name);
        if (processStability)
        {
            DelaneysSlider_Main(processStability);
        }
        else
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
        }


        const parent = SilkFlo.ViewModels.Business.Idea.Modal.GetParent ();
        name = 'NextPage0';
        element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        setTimeout (
            () =>
            {
                element.focus ();
            },
            500 );
    },

    PreviousPage: function ()
    {
        SilkFlo.ViewModels.Business.Idea.Modal.PageNumber--;

        if (HotSpot.Card)
            HotSpot.Card.Close();

        if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === -1)
            SilkFlo.ViewModels.Business.Idea.Modal.PageNumber = SilkFlo.ViewModels.Business.Idea.Modal.PageCount -1;

        SilkFlo.ViewModels.Business.Idea.Modal.Move();
    },

    //SilkFlo.ViewModels.Business.Idea.Modal.PreviousPageOnKeyDown
    PreviousPageOnKeyDown: function (event)
    {
        if (event.code === 'Tab' && event.shiftKey)
        {
            console.log('back');
            SilkFlo.ViewModels.Business.Idea.Modal.PreviousPage();
        }
    },

    //SilkFlo.ViewModels.Business.Idea.Modal.NextPageOnKeyDown
    NextPageOnKeyDown: function (event)
    {
        event.preventDefault();
        event.stopPropagation();

        if (event.code === 'Tab' || event.code === 'Space')
        {
            SilkFlo.ViewModels.Business.Idea.Modal.UpdateRatingScreen();
            SilkFlo.ViewModels.Business.Idea.Modal.NextPage ();
        }
    },

    //SilkFlo.ViewModels.Business.Idea.Modal.NextPageGetGaugeComponentOnKeyDown
    NextPageGetGaugeComponentOnKeyDown: function (event)
    {
        event.preventDefault();
        event.stopPropagation();

        if (event.code === 'Tab' || event.code === 'Space') {
            SilkFlo.Models2.Business.Idea.GetFeasibilityGaugeComponent();
            SilkFlo.Models2.Business.Idea.GetReadinessGaugeComponent();
            SilkFlo.Models2.Business.Idea.GetIdeaGaugeComponent();
            SilkFlo.ViewModels.Business.Idea.Modal.NextPage();
        }
    },

    // SilkFlo.ViewModels.Business.Idea.Modal.NextPage
    NextPage: function ()
    {
        if (HotSpot.Card)
            HotSpot.Card.Close();

        if (!this.ValidatePage(SilkFlo.ViewModels.Business.Idea.Modal.PageNumber))
            return;

        SilkFlo.ViewModels.Business.Idea.Modal.PageNumber++;



        if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === SilkFlo.ViewModels.Business.Idea.Modal.PageCount)
        {
            SilkFlo.ViewModels.Business.Idea.Modal.PageNumber = 0;
        }

        SilkFlo.ViewModels.Business.Idea.Modal.Move();
    },


    MovePage: function (pageNumber)
    {
        SilkFlo.ViewModels.Business.Idea.Modal.PageNumber = pageNumber;
        SilkFlo.ViewModels.Business.Idea.Modal.Move();
    },

    Move: function ()
    {
        const deltaX = SilkFlo.ViewModels.Business.Idea.Modal.Width * SilkFlo.ViewModels.Business.Idea.Modal.PageNumber * -1;

        const matrix = `matrix( 1, 0, 0, 1, ${deltaX}, 0)`;

        if (!SilkFlo.ViewModels.Business.Idea.Modal.ModalSubmitIdeaPages)
            SilkFlo.ViewModels.Business.Idea.Modal.ModalSubmitIdeaPages = document.getElementById('ModalSubmitIdeaPages');


        if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 9)
            SilkFlo.SVGTools.AnimatePaths(SilkFlo.ViewModels.Business.Idea.Modal.ModalSubmitIdeaPages);

        SilkFlo.ViewModels.Business.Idea.Modal.ModalSubmitIdeaPages.style.transform = matrix;


        SilkFlo.ViewModels.Business.Idea.Modal.SetFocus ();
    },


    //SilkFlo.ViewModels.Business.Idea.Modal.SetFocus
    SetFocus: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Modal.GetParent();

        let name = '';
        if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 1)
            name = 'Business.Idea.Name';
        else if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 2)
            name = 'Business.Idea.DepartmentId';
        else if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 3)
            name = 'ButtonBusiness.Idea.RuleId';
        else if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 4)
            name = 'ButtonBusiness.Idea.InputDataStructureId';
        else if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 5)
            name = 'ButtonBusiness.Idea.Rating';
        else if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 6)
            name = 'ButtonBusiness.Idea.DocumentationPresentId';
        else if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 7)
            name = 'AssignProcessOwner';
        else if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 8)
            name = 'btnAddCollaborators';
        else if (SilkFlo.ViewModels.Business.Idea.Modal.PageNumber === 9)
            name = 'btnFinish';
        else
            name = `NextPage${SilkFlo.ViewModels.Business.Idea.Modal.PageNumber}`;

        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Modal.SetFocus: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        setTimeout(
            () => { element.focus(); },
            300);
    },

    ValidatePage: function (pageNumber)
    {
        const logPrefix = "SilkFlo.ViewModels.Business.Idea.Modal.ValidatePage: ";

        const parentElement = document.getElementById('FormModal_BusinessIdea');


        // Guard Clause
        if (!parentElement)
        {
            console.log(logPrefix + 'The parent element with id of "FormModal_BusinessIdea" is missing.');
            return false;
        }


        const noDepartments = parentElement.querySelector ( '[name="Business.Idea.noDepartments"]' );


        if (noDepartments)
        {
            SilkFlo.ViewModels.Business.Idea.Modal.ValidateDepartments = false;
        }
        else
        {
            SilkFlo.ViewModels.Business.Idea.Modal.ValidateDepartments = true;
        }
        


        this.IsValid = true;
        if (pageNumber === 1)
        {
            // Valid Name element
            this.ValidateName(parentElement);


            const summary = SilkFlo.GetElementInParent(parentElement, 'Business.Idea.Summary');

            // Guard Clause
            if (!summary)
            {
                console.log(logPrefix + 'The parent element with name of "Business.Idea.Summary" is missing.');
                return false;
            }


            if (!this.ValidateElement(summary))
            {
                this.IsValid = false;
            }
        }




        if (SilkFlo.ViewModels.Business.Idea.Modal.ValidateDepartments)
        {
            if (pageNumber === 2)
            {
                const department = SilkFlo.GetElementInParent(parentElement, 'Business.Idea.DepartmentId');


                if (!this.ValidateElement(department))
                {
                    this.IsValid = false;
                }
            }
        }
        else
        {
            pageNumber++;
        }


        if (pageNumber === 7)
        {
            const processOwner = SilkFlo.GetElementInParent(parentElement, 'Business.Idea.ProcessOwnerId');

            // Guard Clause
            if (!processOwner)
            {
                console.log(logPrefix + 'The parent element with name of "Business.Idea.ProcessOwnerId" is missing.');
                return false;
            }

            if (!this.ValidateElement(processOwner))
            {
                this.IsValid = false;
            }
        }


        return this.IsValid;
    },



    ValidateName: function (parentElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Modal.ValidateName: ';


        const nameInvalidFeedback = SilkFlo.GetElementInParent(parentElement, 'Business.Idea.Name_InvalidFeedback');

        // Guard Clause
        if (!nameInvalidFeedback)
        {
            console.log(logPrefix + 'The parent element with name of "Business.Idea.Name_InvalidFeedbackName" is missing.');
            return;
        }


        const nameElement = SilkFlo.GetElementInParent(parentElement, 'Business.Idea.Name');

        // Guard Clause
        if (!nameElement)
        {
            console.log(logPrefix + 'The parent element with name of "Business.Idea.Name" is missing.');
            return;
        }

        nameInvalidFeedback.innerHTML = 'Required';

        if (this.ValidateElement(nameElement))
        {
            this.IsValid = true;
            this.CheckUniqueName(nameElement);
        }
        else
        {
            this.IsValid = false;
            nameInvalidFeedback.innerHTML = '* Required';
        }
    },


    CheckUniqueName: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Modal.CheckUniqueName: ';


        const name = element.value;
        if (!name)
        {
            this.IsValid = false;
            return;
        }


        const url = `/api/Business/Idea/CheckUniqueName/${name}`;

        const http = new XMLHttpRequest();
        http.open(
            'GET',
            url,
            false); // false for synchronous request

        http.setRequestHeader('Content-type',
                              'application/x-www-form-urlencoded');


        http.onreadystatechange = function ()
        {
            return function ()
            {
                if (http.readyState === XMLHttpRequest.DONE
                    && http.status === 200)
                {
                    const str = http.responseText;

                    const id = 'FormModal_BusinessIdea';
                    const parent = document.getElementById(id);
                    if (!parent)
                    {
                        console.log(`${logPrefix}Element with id "${id}" missing`);
                        return;
                    }
                    const elementName = 'Business.Idea.Name';
                    element = SilkFlo.GetElementInParent(parent, elementName);

                    const feedbackElementName = 'Business.Idea.Name_InvalidFeedback';
                    const feedbackElement = SilkFlo.GetElementInParent(parent, feedbackElementName);

                    // str is valid status
                    if (str === 'true')
                    {
                        SilkFlo.ViewModels.Business.Idea.Modal.IsValid = true;

                        element.classList.remove('is-invalid');
                    }
                    else
                    {
                        SilkFlo.ViewModels.Business.Idea.Modal.IsValid = false;

                        element.classList.add('is-invalid');
                        feedbackElement.innerHTML = '* The name is already used';
                    }
                }
            };
        }(this);

        http.send();
    },

    ValidateElement: function (element)
    {
        let isValid = true;

        if (element.value === undefined
            || element.value === null
            || element.value.length === 0)
        {
            isValid = false;
            element.classList.add('is-invalid');
        }
        else
        {
            element.classList.remove('is-invalid');
        }

        return isValid;
    },


    // SilkFlo.ViewModels.Business.Idea.Modal.Save
    Save: function ()
    {
        const parent = this.GetParent();

        if (!parent)
            return;

        const model = SilkFlo.Models.Business.Idea.GetModelFromParent(
            parent,
            [
                'Name',
                'Summary',
                'DepartmentId',
                'TeamId',
                'ProcessId',
                'RuleId',
                'InputId',
                'InputDataStructureId',
                'ProcessStabilityId',
                'DocumentationPresentId',
                'ProcessOwnerId',
                'Rating'
            ]);


        model.Collaborators = SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetModel(parent);



        SilkFlo.Models.Business.Idea.Save(
            model,
            SilkFlo.ViewModels.Business.Idea.Modal.UpdateElements,
            SilkFlo.DataAccess.Feedback,
            'ModalSubmitIdeaPages',
            '/api/Business/Idea/Modal/Post'
        );
    },

    // SilkFlo.ViewModels.Business.Idea.Modal.UpdateElements
    UpdateElements: function ()
    {
        window.$('#ModalSubmitIdea').modal('hide');

        $("#MyIdeas").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
        $("#MyTotalInBuild").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
        $("#MyTotalDeployed").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
        $("#MyCollaborations").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
        var myIdeasDiv = document.getElementById("Dashboard.Idaes");
        $(myIdeasDiv).empty();
        myIdeasDiv.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

        var myCollaborationsDiv = document.getElementById("Dashboard.Collaborations");
        $(myCollaborationsDiv).empty();
        myCollaborationsDiv.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

        $.when(
            $.get("/api/Dashboard/Tile/GetMyIdeas"),
            $.get("/api/Dashboard/Tile/GetMyTotalInBuild"),
            $.get("/api/Dashboard/Tile/GetMyTotalDeployed"),
            $.get("/api/Dashboard/Tile/GetMyCollaborations"),
            $.get("/api/Dashboard/GetMyIdeas"),
            $.get("/api/Dashboard/GetMyCollaborations")
        ).done(function (totalIdeasResponse, totalInBuildResponse, totalDeployedResponse, myPCollaborations, myIdeas, myCollaborations) {
            $("#MyIdeas").html(totalIdeasResponse[0]);
            $("#MyTotalInBuild").html(totalInBuildResponse[0]);
            $("#MyTotalDeployed").html(totalDeployedResponse[0]);
            $("#MyCollaborations").html(myPCollaborations[0]);


            var myIdeasDiv = document.getElementById("Dashboard.Idaes");
            $(myIdeasDiv).empty();
            myIdeasDiv.innerHTML = myIdeas[0];

            var myCollaborationsDiv = document.getElementById("Dashboard.Collaborations");
            $(myCollaborationsDiv).empty();
            myCollaborationsDiv.innerHTML = myCollaborations[0];
        });





        //// Close the modal
        //window.$('#ModalSubmitIdea').modal('hide');


        //SilkFlo.DataAccess.UpdateElementFromAttribute('totalIdeas');
        //SilkFlo.DataAccess.UpdateElementFromAttribute('Chart.AutomationProgramPerformance');
        //SilkFlo.DataAccess.UpdateElementFromAttribute('Chart.PipelineBenefitsByStage');
        //SilkFlo.DataAccess.UpdateElementFromAttribute('Business.Idea.Summary');
        //SilkFlo.DataAccess.UpdateElementFromAttribute('Dashboard.Idaes');
        //SilkFlo.DataAccess.UpdateElementFromAttribute('Dashboard.Collaborations');
    },



    // SilkFlo.ViewModels.Business.Idea.Modal.UpdateRatingScreen
    UpdateRatingScreen: function ()
    {
        const parent = this.GetParent();

        if (!parent)
            return;

        const model = SilkFlo.Models.Business.Idea.GetModelFromParent (
            parent,
            ['Name', 'DisplayName']
        );

        parent.ModelElements.DisplayName.innerHTML = model.Name;
    }
};