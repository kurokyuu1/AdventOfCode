﻿using AdventOfCode.WinUI.Helper;
using AdventOfCode.WinUI.ViewModels;

namespace AdventOfCode.WinUI.Utils;

public class Constants
{
    public const string ExternalEnumsNamespace = "AdventOfCode.Extensions.ExternalEnums";
}

public class ViewModelLocator
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelLocator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public MainWindowViewModel MainWindowViewModel => DiManager.GetService<MainWindowViewModel>();
}
