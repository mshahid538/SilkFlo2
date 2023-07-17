if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};


SilkFlo.ViewModels.User = {

    UpdateUIComponents: function ()
    {
        SilkFlo.DataAccess.UpdateElementFromAttribute('userProfileSummary');
    },


    Search: function (
        searchText,
        page,
        pageCount,
        targetElementId,
        pageSize = 10)
    {
        const url = `/api/user/search/${searchText}/page/${page}/PageCount/${pageCount}/PageSize/${pageSize}`;

        SilkFlo.DataAccess.UpdateElementById (
            url,
            null,
            targetElementId );
    },
};

SilkFlo.ViewModels.User.TeamMember =
{
    // SilkFlo.ViewModels.User.TeamMember.GetParent
    GetParent: function ()
    {
        const id = 'TeamMember.Body';
        const parent = document.getElementById(id);

        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.User.TeamMember.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },



    // SilkFlo.ViewModels.User.TeamMember.ShowMessage
    ShowMessage: function (innerHtml)
    {
        const parent = this.GetParent();
        if (!parent)
            return;

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.User.TeamMember.ShowMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = innerHtml;
    },


    // SilkFlo.ViewModels.User.TeamMember.Invite
    Invite: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.User.TeamMember.Invite: ';


        const parent = this.GetParent();
        if (!parent)
            return;


        const id = 'ModalInviteTeamMember';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        const name = 'Modal.Invite.TeamMember.Send';
        const elementButton = element.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementButton)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        elementButton.disabled = false;


        const url = '/api/User/GetInviteTeamMemberModalBody';
        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            parent,
        );
    },

    //SilkFlo.ViewModels.User.TeamMember.Post
    Post: function ()
    {

        const parent = this.GetParent();
        if (!parent)
            return;

        this.ShowMessage('');

        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            [
                'FirstName',
                'LastName',
                'EmailPrefix'],
            parent.id + '.');

        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.ViewModels.User.TeamMember.PostCallBack,
            SilkFlo.DataAccess.Feedback,
            parent.id,
            '/api/User/PostInviteTeamMember');
    },


    // SilkFlo.ViewModels.User.TeamMember.PostCallBack
    PostCallBack: function (message)
    {
        const id = 'ModalInviteTeamMember';
        const parent = document.getElementById(id);

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        SilkFlo.DataAccess.Feedback(message, parent.id );

        const name = 'Modal.Invite.TeamMember.Send';
        const elementButton = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementButton)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        elementButton.disabled = true;

        setTimeout(SilkFlo.ViewModels.User.TeamMember.CloseModal, 1500);
    },


    // SilkFlo.ViewModels.User.TeamMember.CloseModal
    CloseModal: function ()
    {
        const element = document.getElementById('ModalInviteTeamMember');
        const modal = bootstrap.Modal.getInstance(element );
        modal.hide();
    }
};