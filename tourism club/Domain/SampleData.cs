using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Models;
using tourism_club.Domain.Interfaces;
using tourism_club.Domain.Classes;

namespace tourism_club.Domain
{
    public static class SampleData
    {
        
        public static void Initialize(AppDBContent context)
        {
            if (!context.locations.Any())
            {
                context.locations.AddRange(
                    new Location
                    {
                        LocationTitle = "Ратуша Івано-Франківська",
                        LocationDescription = "Протягом 1920‑х років у Польщі вирувала економічна криза і тому понад 10 років довелося чекати, доки міська влада візьметься за будівлю. У 1929 році гроші нарешті знайшли, і в грудні був прийнятий кошторис будівництва. Фірма «Інженер Крауш і спілка», яка перемогла у конкурсі, розпочала роботу. Архітектором був інженер Станіслав Треля. Вже за традицією, у фундамент південно-східного крила помістили капсулу з планами новобудови, фотографіями старої ратуші та іншими документами. Завершити будівництво передбачали у 1932 році. Але через хронічний брак коштів споруду звели на три роки пізніше, а внутрішні оздоблювальні роботи тривали аж до 1939 року.",
                        PathToPhotos = "/images/РатушаІФ.jpg",
                        comments = null
                    },
                    new Location
                    {
                        LocationTitle = "Ратуша Коломия",
                        LocationDescription = "Протягом 1920‑х років у Польщі вирувала економічна криза і тому понад 10 років довелося чекати, доки міська влада візьметься за будівлю. У 1929 році гроші нарешті знайшли, і в грудні був прийнятий кошторис будівництва. Фірма «Інженер Крауш і спілка», яка перемогла у конкурсі, розпочала роботу. Архітектором був інженер Станіслав Треля. Вже за традицією, у фундамент південно-східного крила помістили капсулу з планами новобудови, фотографіями старої ратуші та іншими документами. Завершити будівництво передбачали у 1932 році. Але через хронічний брак коштів споруду звели на три роки пізніше, а внутрішні оздоблювальні роботи тривали аж до 1939 року.",
                        PathToPhotos = "/images/РатушаКоломия.jpg",
                        comments = null
                    }
                   ) ; 
            }
            if (!context.frames.Any())
            {
                
                context.frames.AddRange(
                    new Frame
                    {
                        source = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d463.4431859255984!2d24.71076078438212!3d48.92243064737077!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4730c1134ecde1fd%3A0xc4573d3ae24e5d27!2z0JjQstCw0L3Qvi3QpNGA0LDQvdC60L7QstGB0LrQsNGPINGA0LDRgtGD0YjQsA!5e0!3m2!1sru!2sua!4v1636900228728!5m2!1sru!2sua",
                        width = "600",
                        height = "450",
                        style = "border:0;",
                        screen = "",
                        loading = "lazy",
                        LocationId = 1
                    },
                    new Frame
                    {
                        source = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3142.375242593979!2d25.031664173694516!3d48.52533411712299!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4736d3af9f2342e9%3A0xec2f5a85cb3ff4a6!2sKolomyia%20Town%20Hall!5e0!3m2!1sru!2sua!4v1636900550438!5m2!1sru!2sua",
                        width = "600",
                        height = "450",
                        style = "border:0;",
                        screen = "",
                        loading = "lazy",
                        LocationId = 2
                    }
                 );

            }
            if (!context.comments.Any())
            {
                context.comments.AddRange(
                    new Comment
                    {
                        comment = "Там недавно трахались на ратуші, хто зняв?",
                        LocationId = 1,
                        CommentatorId = 1
                    },
                    new Comment
                    {
                        comment = "Продам жигуль недорого",
                        LocationId = 1,
                        CommentatorId = 2
                    }
                 );
            }
            context.SaveChanges();
        }
    }
}
