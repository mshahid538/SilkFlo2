if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Explore)
    SilkFlo.ViewModels.Explore = {};


SilkFlo.ViewModels.Explore.People = {


    SearchPeople: function (
        searchText,
        page,
        targetElementId,
        insertFirstId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Explore.People.SearchPeople: ';


        if (!searchText)
        {
            const id = 'Explore.People.Search';
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


        let url = '/api/Explore/People/Table';
        if (isSearchText)
        {
            url += '/Search/' + searchText;
        }

        if (isPage)
        {
            url += '/page/' + page;


            const id = 'Explore.People.Table.SelectedPage';
            const selectedPageElement = document.getElementById(id);

            //Guard Clause
            if (!selectedPageElement)
            {
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }

            selectedPageElement.value = page;
        }

        if (insertFirstId)
        {
            url += '/FirstId/' + insertFirstId;
        }

        SilkFlo.DataAccess.UpdateElementById (
            url,
            null,
            targetElementId );
    },
};