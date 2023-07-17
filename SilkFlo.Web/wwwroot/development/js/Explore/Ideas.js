if (SilkFlo.Explore === undefined)
    SilkFlo.Explore = {};

if (SilkFlo.Explore.Ideas === undefined)
    SilkFlo.Explore.Ideas = {};


SilkFlo.Explore.Ideas = {

    IsSavingFollow: false,
    IsSavingVote: false,

    Follow_Click: function (element)
    {
        if (element.IsSaving !== undefined
         && element.IsSaving)
        {
            return;
        }


        this.UpdateDatabase(element, '/api/Idea/PostFollow');
    },

    Vote_Click: function (element)
    {
        if (element.IsSaving !== undefined
            && element.IsSaving)
        {
            return;
        }


        this.UpdateDatabase(element, '/api/Idea/PostVote');
    },

    UpdateButton: function (element)
    {
        const counter = element.querySelectorAll('span')[0];
        let count = parseInt(counter.innerHTML);

        const isSelected = element.classList.contains('selected');
        console.log(count);
        if (isSelected)
        {
            element.classList.remove('selected');
            count--;
        }
        else
        {
            element.classList.add('selected');
            count++
        }

        counter.innerHTML = count;

        return !isSelected;
    },

    UpdateDatabase: function (element, url)
    {
        const http = new XMLHttpRequest();
        http.open(
            'POST',
            url,
            true); // false for synchronous request

        http.setRequestHeader(
            'Content-type',
            'application/json');


        http.onreadystatechange = function (element)
        {
            return function ()
            {
                if (http.readyState === XMLHttpRequest.DONE
                    && http.status === 200)
                {
                    element.IsSaving = false;
                }
            };
        }(element);


        const ideaId = element.getAttribute('silkflo-ideaId');
        const isSelected = this.UpdateButton(element);

        element.IsSaving = true;
        const data = '{ "ideaId": "' + ideaId + '","isSelected": ' + isSelected + '}';
        http.send(data);
    },
};