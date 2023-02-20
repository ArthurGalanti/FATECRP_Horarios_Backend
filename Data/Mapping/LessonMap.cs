using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeleniumBot.Models;

namespace SeleniumBot.Data.Mapping;

public class LessonMap : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("FatecHorarios");
        
        builder.Property(x => x.Course)
            .HasColumnName("curso");
        
        builder.Property(x => x.Semester)
            .HasColumnName("periodo");
        
        builder.Property(x => x.SubjectName)
            .HasColumnName("disciplina");
        
        builder.Property(x => x.TagSiga)
            .HasColumnName("tag_siga");
        
        builder.Property(x => x.WeekDay)
            .HasColumnName("dia_da_semana");
        
        builder.Property(x => x.LessonTime)
            .HasColumnName("horario");
        
        builder.Property(x => x.TeacherName)
            .HasColumnName("professor");
        
        builder.Property(x => x.Classroom)
            .HasColumnName("sala");
        
        builder.Property(x => x.Floor)
            .HasColumnName("andar");
        
        builder.Property(x => x.LocationURL)
            .HasColumnName("localizacao");

    }
}