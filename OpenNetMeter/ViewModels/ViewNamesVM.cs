using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using OpenNetMeter.Models;

namespace OpenNetMeter.ViewModels;

public class ViewNamesVM : INotifyPropertyChanged
{
    private Visibility isVisible;
    public Visibility IsVisible
    {
        get { return isVisible; }
        set
        {
            isVisible = value;
            OnPropertyChanged("IsVisible");
        }
    }
    public ViewNamesVM()
    {
        IsVisible = Visibility.Hidden;
    }
    public object getNames(string name)
    {
        var SelectedProfile = "WiFi 2(HUAWEI-5G-vp3X)";
        using (ApplicationDB dB = new ApplicationDB(SelectedProfile, new string[] { "Read Only=True" }))
        {
            object obj = dB.getProcessName(name);
            return obj;
        }

    }
    public int setPreferedName(string Orignalname, string PreferedName)
    {
        var SelectedProfile = "WiFi 2(HUAWEI-5G-vp3X)";
        using (ApplicationDB dB = new ApplicationDB(SelectedProfile, new string[] { "Read Only=False" }))
        {
            try
            {
                List<object> names = new List<object>();
                //check if there is already a process with its original name as the new one
                object pn = dB.getProcessName(PreferedName);

                if (pn.ToString() == PreferedName)
                {
                    dB.SetPreferredName(Orignalname, PreferedName);
                    return 1;

                }
                return 0;

            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

}
