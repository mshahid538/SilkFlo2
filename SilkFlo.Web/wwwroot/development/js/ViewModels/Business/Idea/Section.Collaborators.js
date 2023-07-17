// Add the model
if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};

if (!SilkFlo.ViewModels.Business.Idea.Section)
    SilkFlo.ViewModels.Business.Idea.Section = {};

SilkFlo.ViewModels.Business.Idea.Section.Collaborators = {
    IdeaId: null,


    // SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetParent
    GetParent: function ()
    {
        const id = 'Business.Idea.Section.Collaborators.Content';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },

    // SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Edit
    Edit: function (element, id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Edit: ';

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        // Guard Clause
        if (!id) {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }


        SilkFlo.DisableButton(element);


        this.IdeaId = id;


        const parent = SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetParent();
        if (!parent)
            return;


        const url = `/api/Idea/Collaborator/Edit/${id}`;
        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            parent);
    },


    // Get the About Section
    GetModel: function (parent)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetModel: ';

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}parentElement parameter missing`);
            return null;
        }

        let name = 'Business.Idea.Collaborators';
        const collaboratorElements = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!collaboratorElements)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return null;
        }

        const userModels = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            collaboratorElements,
            'User',
            [
                'Id',
                'FirstName',
                'LastName'
            ],
            'User.');


        const collaborators = [];

        const length = collaboratorElements.children.length;
        for (let i = 0; i < length; i++)
        {
            const element = collaboratorElements.children[i];

            const userModel = SilkFlo.Models.Abstract.GetModelFromParent(
                element,
                ['Id'],
                'User.');


            name = 'Business.Roles';
            const roleElements = element.querySelector(`[name="${name}"]`);

            const roleModels = SilkFlo.Models.Abstract.GetModelCollectionFromParent
                (
                    roleElements,
                    'Business.Role',
                    ['Id', 'Name'],
                    'Business.Role.'
                );


            //const lengthK = roleModels.length;
            //for (let k = 0; k < lengthK; k++)
            //{
            //    const roleModel = roleModels[k];
            //    roleModel.CollaboratorId = userModel.Id;
            //}

            element.Model.Roles = roleModels;

            const model = userModels[i];

            const collaborator = {
                UserId: model.Id,
                IdeaId: this.IdeaId
            };
            collaborators.push(collaborator);

            collaborator.CollaboratorRoles = [];


            const roleLength = roleModels.length;
            for (let j = 0; j < roleLength; j++)
            {
                const role = roleModels[j];

                const collaboratorRole =
                {
                    RoleId: role.Id,
                    CollaboratorId: userModel.Id
                };

                collaborator.CollaboratorRoles.push(collaboratorRole);
            }
        }

        return collaborators;
    },


    // Save the Collaborators Section
    // SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Save
    Save: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Save: ';

        const parent = SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetParent();
        if (!parent)
            return;

        // Guard Clause
        if (!this.IdeaId)
        {
            console.log(`${logPrefix}IdeaId missing`);
            return;
        }


        const collaborators = this.GetModel(parent);

        // Guard Clause
        if (!collaborators)
        {
            console.log
                (`${logPrefix}model missing`);
            return;
        }

        this.Collaborators = collaborators;

        this.Parent = parent;

        SilkFlo.Models.Abstract.Save
            (
                collaborators,
                SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Save_Callback,
                SilkFlo.DataAccess.Feedback,
                parent.id,
                `/api/Business/Idea/Section/Collaborators/Post/IdeaId/${this.IdeaId}`,
                'POST');
    },

    // SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Save_Callback
    Save_Callback: function (str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Save_Callback: ';

        const container = SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetParent();
        if (!container)
            return;

        container.innerHTML = str;


        const parent = SilkFlo.Business.Idea.Summary.GetParent ();

        const name = 'Business.Idea.Section.Collaborators.Count';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Collaborators.length;
    },


    // SilkFlo.ViewModels.Business.Idea.Section.Collaborators.SaveFailed_Callback
    SaveFailed_Callback: function (str)
    {
        if (!SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.Collaborators.SaveFailed_Callback: ';
            console.log
                (`${logPrefix}Parent missing`);
            return;
        }

        let text = 'Save failed';

        if (str)
        {
            const o = JSON.parse(str);
            text = o.error[0];
        }

        const parent = SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetParent();
        if (!parent)
            return;

        parent.innerHTML = text;
    },

    // Cancel the Collaborators Section

    // SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Cancel
    Cancel: function (id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.Collaborators.Cancel: ';

        // Guard Clause
        if (!id) {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }

        const parent = SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetParent();
        if (!parent)
            return;


        const url = `/api/Business/Idea/Section/Collaborators/Detail/ideaid/${id}`;
        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            parent,
            '',
            'GET',
            SilkFlo.ViewModels.Business.Idea.Section.ScrollTop,
            SilkFlo.DataAccess.Feedback);
    }
};