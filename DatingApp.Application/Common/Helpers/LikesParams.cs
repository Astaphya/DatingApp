﻿namespace DatingApp.Application.Common.Helpers;

public class LikesParams : PaginationParams
{
    public int UserId { get; set; }
    public string? Predicate { get; set; }
}