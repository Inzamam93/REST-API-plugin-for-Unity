using System;
using System.Collections.Generic;

[Serializable]
public class JsonData
{
    public List<ResultsList> results;
    public List<InfoList> info;
}

public class ResultsList
{
    public string gender;
    public List<string> name;
    public List<string> location;
    public string email;
    public List<string> login;
    public List<string> dob;
    public string registered;
    public string phone;
    public string cell;
    public List<string> id;
    public List<string> picture;
    public string nat;

}

public class InfoList
{
    public string seed;
    public int results;
    public int page;
    public float version; 
}