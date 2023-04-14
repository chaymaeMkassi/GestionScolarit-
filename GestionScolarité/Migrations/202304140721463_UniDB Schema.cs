namespace GestionScolarité.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniDBSchema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Users");
            DropForeignKey("dbo.TeacherSections", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
            DropIndex("dbo.TeacherSections", new[] { "SubjectId" });
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.SubjectId);
            
            AddColumn("dbo.Sections", "SectionName", c => c.String());
            AddColumn("dbo.Subjects", "SubjectName", c => c.String());
            AddColumn("dbo.TeacherSections", "SectionId", c => c.Int(nullable: false));
            CreateIndex("dbo.TeacherSections", "SectionId");
            AddForeignKey("dbo.TeacherSections", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
            DropColumn("dbo.Sections", "Name");
            DropColumn("dbo.Subjects", "Name");
            DropColumn("dbo.Subjects", "TeacherId");
            DropColumn("dbo.TeacherSections", "SubjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeacherSections", "SubjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "TeacherId", c => c.Int());
            AddColumn("dbo.Subjects", "Name", c => c.String());
            AddColumn("dbo.Sections", "Name", c => c.String());
            DropForeignKey("dbo.TeacherSubjects", "TeacherId", "dbo.Users");
            DropForeignKey("dbo.TeacherSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSections", "SectionId", "dbo.Sections");
            DropIndex("dbo.TeacherSubjects", new[] { "SubjectId" });
            DropIndex("dbo.TeacherSubjects", new[] { "TeacherId" });
            DropIndex("dbo.TeacherSections", new[] { "SectionId" });
            DropColumn("dbo.TeacherSections", "SectionId");
            DropColumn("dbo.Subjects", "SubjectName");
            DropColumn("dbo.Sections", "SectionName");
            DropTable("dbo.TeacherSubjects");
            CreateIndex("dbo.TeacherSections", "SubjectId");
            CreateIndex("dbo.Subjects", "TeacherId");
            AddForeignKey("dbo.TeacherSections", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subjects", "TeacherId", "dbo.Users", "Id");
        }
    }
}
