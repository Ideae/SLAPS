﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration3wpf
{
    public class Student : User
    {
        //File[] getInstructionDocs();
        //List<Course> getCourses() { }
        //List<Project> getProjects() { }
        //List<Submission> getSubmissions() { }

        public List<Course> courses { get; set; }
        public List<Project> projects { get; set; }
        public List<Submission> submissions { get; set; }

        public void requestPWReset() { }
        public void submitAssignment() { }
    }
}