using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webform.Models
{
    public class createContactresponseDTO
    {
        public List<Message> Messages { get; set; }
        public Result Result { get; set; }
    }
    public class Message
    {
        public string MessageID { get; set; }
        public string RelatedObjectGUID { get; set; }
        public string RelatedObjectType { get; set; }
        public bool IsError { get; set; }
        public string MessageDescription { get; set; }
    }

    public class Manager
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public bool HasProfilePicture { get; set; }
        public string UserType { get; set; }
        public bool IsMe { get; set; }
        public bool Retired { get; set; }
        public string GUID { get; set; }
    }

    public class ExtendedProperties
    {
    }

    public class WorkingHoursCalendar
    {
        public int AverageWorkingHoursPerDay { get; set; }
        public string Timezone { get; set; }
        public bool Retired { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string GUID { get; set; }
    }

    public class RobotFarm
    {
        public string Technology { get; set; }
        public string Name { get; set; }
        public string GUID { get; set; }
    }

    public class UserGroup
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string GUID { get; set; }
    }

    public class Result
    {
        public bool IsOnline { get; set; }
        public string  CompanyGUID { get; set; }
        public string KnownAs { get; set; }
        public float CostPerUnitTime { get; set; }
        public string EmployeeID { get; set; }
        public DateTime ? DateOfBirth { get; set; }
        public string Timezone { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileTelephoneNumber { get; set; }
        public string OfficeLocation { get; set; }
        public string Address { get; set; }
        public bool PasswordReset { get; set; }
        public DateTime ? PasswordLastChanged { get; set; }
        public string PreferredDateFormat { get; set; }
        public string PreferredTimeFormat { get; set; }
        public string PreferredLanguage { get; set; }
        public Manager Manager { get; set; }
        public string RPAState { get; set; }
        public ExtendedProperties ExtendedProperties { get; set; }
        public string SuspendedState { get; set; }
        public WorkingHoursCalendar WorkingHoursCalendar { get; set; }
        public List<RobotFarm> RobotFarms { get; set; }
        public string AllowedPacketTypes { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public string CompanyName { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime ? LastLogon { get; set; }
        public bool ? IsTeamLeader { get; set; }
        public bool ? CanCreate { get; set; }
        public bool ? CanReassign { get; set; }
        public bool ? CanAccessBuilder { get; set; }
        public bool ? CanSetLiveBuilder { get; set; }
        public bool ? CanEditSharedConfiguration { get; set; }
        public bool ? CanAccessUserManagement { get; set; }
        public bool ? CanSeePeerAndQueues { get; set; }
        public bool ? IsSSOOnly { get; set; }
        public string RPAID { get; set; }
        public string TotalCapacity { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public bool HasProfilePicture { get; set; }
        public string UserType { get; set; }
        public bool IsMe { get; set; }
        public bool Retired { get; set; }
        public string GUID { get; set; }
    }
}