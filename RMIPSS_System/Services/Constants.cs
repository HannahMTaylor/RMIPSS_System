﻿namespace RMIPSS_System.Services;

public static class Constants
{
    public const string ROLE_SCHOOL_USER = "ROLE_SCHOOL_USER";
    public const string ROLE_STATE_USER = "ROLE_STATE_USER";
    public const string ROLE_STATE_AND_SCHOOL_USER = ROLE_SCHOOL_USER + "," + ROLE_STATE_USER;

    public const string LANDING_URL_AFTER_LOGIN = "/Student/ListStudent";
}
