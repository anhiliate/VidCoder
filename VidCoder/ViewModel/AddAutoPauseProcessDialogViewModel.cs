﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using VidCoder.Services;
using Microsoft.Practices.Unity;
using System.Windows.Input;

namespace VidCoder.ViewModel
{
	public class AddAutoPauseProcessDialogViewModel : OkCancelDialogViewModel
	{
		private string processName;
		private ObservableCollection<string> currentProcesses;
		private string selectedProcess;
		private IProcesses processes;

		private ICommand refreshCurrentProcessesCommand;

		public AddAutoPauseProcessDialogViewModel()
		{
			this.processes = Unity.Container.Resolve<IProcesses>();
			this.currentProcesses = new ObservableCollection<string>();
			this.RefreshCurrentProcesses();
		}

		public string ProcessName
		{
			get
			{
				return this.processName;
			}

			set
			{
				this.processName = value;
				this.NotifyPropertyChanged("ProcessName");
			}
		}

		public ObservableCollection<string> CurrentProcesses
		{
			get
			{
				return this.currentProcesses;
			}
		}

		public string SelectedProcess
		{
			get
			{
				return this.selectedProcess;
			}

			set
			{
				this.selectedProcess = value;
				this.ProcessName = value;
				this.NotifyPropertyChanged("SelectedProcess");
			}
		}

		public ICommand RefreshCurrentProcessesCommand
		{
			get
			{
				if (this.refreshCurrentProcessesCommand == null)
				{
					this.refreshCurrentProcessesCommand = new RelayCommand(param =>
					{
						this.RefreshCurrentProcesses();
					});
				}

				return this.refreshCurrentProcessesCommand;
			}
		}

		private void RefreshCurrentProcesses()
		{
			Process[] processes = this.processes.GetProcesses();
			this.currentProcesses.Clear();

			foreach (Process process in processes)
			{
				this.currentProcesses.Add(process.ProcessName);
			}
		}
	}
}