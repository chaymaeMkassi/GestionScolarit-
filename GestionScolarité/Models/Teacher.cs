///////////////////////////////////////////////////////////
//  teacher.cs
//  Implementation of the Class teacher
//  Generated by Enterprise Architect
//  Created on:      29-Mar-2023 8:57:10 PM
//  Original author: dell xps
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class Teacher : Personne {

	public int id { get; set; }
    public Section[] m_section { get; set; }
    public Subject[] m_subject { get; set; }

   
}//end teacher