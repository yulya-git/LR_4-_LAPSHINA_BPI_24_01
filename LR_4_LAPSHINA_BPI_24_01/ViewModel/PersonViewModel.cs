using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using LR_4_LAPSHINA_BPI_24_01.Helper;
using LR_4_LAPSHINA_BPI_24_01.Model;
using LR_4_LAPSHINA_BPI_24_01.View;

namespace LR_4_LAPSHINA_BPI_24_01.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> ListPerson { get; set; } = new ObservableCollection<Person>();
        public ObservableCollection<PersonDpo> ListPersonDpo { get; set; } = new ObservableCollection<PersonDpo>();

        private PersonDpo selectedPersonDpo;
        public PersonDpo SelectedPersonDpo
        {
            get { return selectedPersonDpo; }
            set { selectedPersonDpo = value; OnPropertyChanged("SelectedPersonDpo"); }
        }

        public PersonViewModel()
        {
            // Начальные данные
            ListPerson.Add(new Person { Id = 1, RoleId = 1, FirstName = "Иван", LastName = "Иванов", Birthday = new DateTime(1980, 02, 28) });
            ListPerson.Add(new Person { Id = 2, RoleId = 2, FirstName = "Петр", LastName = "Петров", Birthday = new DateTime(1981, 03, 20) });

            GetListPersonDpo();
        }

        public void GetListPersonDpo()
        {
            ListPersonDpo.Clear();
            foreach (var person in ListPerson)
            {
                PersonDpo p = new PersonDpo();
                p = p.CopyFromPerson(person);
                ListPersonDpo.Add(p);
            }
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListPerson)
            {
                if (max < r.Id) max = r.Id;
            }
            return max;
        }

        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                return addPerson ?? (addPerson = new RelayCommand(obj =>
                {
                    WindowNewEmployee wnPerson = new WindowNewEmployee { Title = "Новый сотрудник" };
                    int maxIdPerson = MaxId() + 1;
                    PersonDpo per = new PersonDpo { Id = maxIdPerson, Birthday = DateTime.Now };
                    wnPerson.DataContext = per;

                    if (wnPerson.ShowDialog() == true)
                    {
                        // Безопасное приведение типов
                        if (wnPerson.CbRole.SelectedItem is Role r)
                        {
                            per.RoleName = r.NameRole;
                            ListPersonDpo.Add(per);

                            Person p = new Person();
                            p = p.CopyFromPersonDpo(per);
                            p.RoleId = r.Id;
                            ListPerson.Add(p);
                        }
                        else
                        {
                            MessageBox.Show("Необходимо выбрать должность!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }, (obj) => true));
            }
        }

        private RelayCommand editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                return editPerson ?? (editPerson = new RelayCommand(obj =>
                {
                    WindowNewEmployee wnPerson = new WindowNewEmployee { Title = "Редактирование данных сотрудника" };
                    PersonDpo personDpo = SelectedPersonDpo;
                    PersonDpo tempPerson = personDpo.ShallowCopy();
                    wnPerson.DataContext = tempPerson;

                    if (wnPerson.ShowDialog() == true)
                    {
                        if (wnPerson.CbRole.SelectedItem is Role r)
                        {
                            personDpo.RoleName = r.NameRole;
                            personDpo.FirstName = tempPerson.FirstName;
                            personDpo.LastName = tempPerson.LastName;
                            personDpo.Birthday = tempPerson.Birthday;

                            FindPerson finder = new FindPerson(personDpo.Id);
                            List<Person> listPerson = ListPerson.ToList();
                            Person p = listPerson.Find(new Predicate<Person>(finder.PersonPredicate));

                            if (p != null) // Проверка, что сотрудник найден
                            {
                                p.RoleId = r.Id;
                                p.FirstName = personDpo.FirstName;
                                p.LastName = personDpo.LastName;
                                p.Birthday = personDpo.Birthday;
                            }
                        }
                    }
                }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }

        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                return deletePerson ?? (deletePerson = new RelayCommand(obj =>
                {
                    PersonDpo person = SelectedPersonDpo;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по сотруднику: \n" + person.LastName + " " + person.FirstName, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                    {
                        ListPersonDpo.Remove(person);
                        FindPerson finder = new FindPerson(person.Id);
                        List<Person> listPerson = ListPerson.ToList();
                        Person p = listPerson.Find(new Predicate<Person>(finder.PersonPredicate));

                        if (p != null) // Проверка, что сотрудник найден перед удалением
                        {
                            ListPerson.Remove(p);
                        }
                    }
                }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}