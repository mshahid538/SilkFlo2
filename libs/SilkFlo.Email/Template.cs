using System;

namespace SilkFlo.Email
{
  public enum Template
  {
    ReferrerInvitation = 14, // 0x0000000E
    ResellerInvitation = 15, // 0x0000000F
    TenantInvitation = 17, // 0x00000011
    TeamMemberInvitation = 18, // 0x00000012
    EmailChangedConfirmation = 19, // 0x00000013
    EmailConfirmation = 20, // 0x00000014
    PasswordReset = 21, // 0x00000015
    NotifyRoleChange = 22, // 0x00000016
    CancelSubscription = 23, // 0x00000017
    CancelSubscriptionAgency = 24, // 0x00000018
    WelcomeEmail1 = 26, // 0x0000001A
    InvoicePaid = 27, // 0x0000001B
  }
}
