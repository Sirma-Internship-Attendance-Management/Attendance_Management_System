namespace Attendance_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        AttendanceId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        CheckOutTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.AttendanceId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        AdminUsername = c.String(),
                        AdminPassword = c.String(),
                        /*  //new Information added
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Logo = c.Byte(nullable: true),*/
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.String(),
                        ContactDetails = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
            DropTable("dbo.Attendances");
        }
    }
}
