if (!SilkFlo)
    SilkFlo = {};

if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

SilkFlo.ViewModels.Workshop = {

    // SilkFlo.ViewModels.Workshop.Url
    Url: '',

    // SilkFlo.ViewModels.Workshop.GetParent
    GetParent: function ()
    {
        const id = 'WorkshopContent';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Workshop.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return element;
    },

    // SilkFlo.ViewModels.Workshop.GetContent
    GetContent: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Workshop.GetContent: ';

        // Guard Clause
        if (!element)
        {
            console.log(logPrefix + 'element parameter missing');
            return;
        }


        const parent = SilkFlo.ViewModels.Workshop.GetParent();
        if (!parent)
            return;


        const parentElement = element.parentElement;
        const children = parentElement.children;
        const length = children.length;

        for (let i = 0; i < length; i++)
        {
            children[i].classList.remove('active');
        }

        element.classList.add('active');
        let url = element.getAttribute('url');

        if (!url)
        {
            console.log(logPrefix + 'url attribute missing');
            return;
        }


        parent.setAttribute (
            'url',
            url );

        SilkFlo.ViewModels.Workshop.Url = url;

        const apiUrl = `/api/Workshop/StageGroup/id/${url}`;
        url = `/Workshop/StageGroup/${url}`;



        SilkFlo.DataAccess.UpdateElement (
            apiUrl,
            '',
            parent,
            url,
            'GET',
            SilkFlo.ViewModels.Workshop.GetContent_Callback);
    },

    // SilkFlo.ViewModels.Workshop.GetContent_Callback
    GetContent_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Workshop.GetContent_Callback: ';

        const parent = SilkFlo.ViewModels.Workshop.GetParent();
        if (!parent)
            return;

        SilkFlo.DataAccess.GetSubComponents(parent);

        if (!SilkFlo.ViewModels.Workshop.Url)
        {
            console.log('SilkFlo.ViewModels.Workshop.Url missing');
            return;
        }

        const id = 'Business.Idea.Summary';
        const elementIdeaSummary = document.getElementById(id);

        // Guard Clause
        if (!elementIdeaSummary) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        elementIdeaSummary.setAttribute (
            SilkFlo.DataAccess.AttributeName.Url,
            `Business/Idea/GetSummary/GroupName/${SilkFlo.ViewModels.Workshop.Url}`);

        elementIdeaSummary.setAttribute (
            SilkFlo.DataAccess.AttributeName.GetManually,
            '' );
    },

    // SilkFlo.ViewModels.Workshop.SelectIdea
    SelectIdea: function (id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Workshop.SelectIdea: ';

        // Guard Clause
        if (!id)
        {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }


        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        const parent = element.parentElement;

        parent.insertBefore(
            element,
            parent.firstChild);

        element.classList.add( 'transition' );
        element.classList.add( 'pulse-background-green' );
    },

    Search: function (searchText,
                      page,
                      targetElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Workshop.Search: ';



        if (!searchText)
        {
            const id = 'Workshop.Pipeline.Deployed.Search';
            const element = document.getElementById(id);

            //Guard Clause
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



        let url = '/api/Workshop/Deployed/Table';
        if (isSearchText)
        {
            url += `/Search/${searchText}`;
        }

        if (isPage)
        {
            url += `/page/${page}`;


            const id = 'Workshop.Pipeline.Deployed.SelectedPage';
            const selectedPageElement = document.getElementById(id);

            //Guard Clause
            if (!selectedPageElement)
            {
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }

            selectedPageElement.value = page;
        }


        SilkFlo.DataAccess.UpdateElementById(
            url,
            null,
            targetElementId);
    },

    Build: {
        // SilkFlo.ViewModels.Workshop.Build.YearFilter
        YearFilter: function (element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Workshop.Build.YearFilter: ';


            // Guard Clause
            if (!element) {
                console.log(`${logPrefix}element parameter missing`);
                return;
            }

            const parent = SilkFlo.ViewModels.Workshop.GetParent();
            if (!parent)
                return;


            const name = 'Chart.AutomationBuildPipeline';
            const target = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!target) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            let url = `Workshop/GetAutomationBuildPipeline/year/${element.value}`;

           target.setAttribute (
               SilkFlo.DataAccess.AttributeName.Url,
               url );

           url = `/api/${url}`;

            SilkFlo.DataAccess.UpdateElement(
                url,
                null,
                target);
        }
    }
};