using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using NotesListTask.Model;
using NotesListTask.View;

namespace NotesListTask.ViewModel
{
    internal class ApplicationViewModel
    {
        ApplicationContext _dataBase = new ApplicationContext();

        RelayCommand _addCommand;
        RelayCommand _editCommand;
        RelayCommand _deleteCommand;
        RelayCommand _deleteInsideListCommand;

        public ObservableCollection<Note> Notes { get; set; }

        public ApplicationViewModel()
        {
            _dataBase.Database.EnsureCreated();
            _dataBase.Notes.Load();
            Notes = _dataBase.Notes.Local.ToObservableCollection();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand((obj) =>
                {
                    NoteWindow noteWindow = new NoteWindow(new Note());
                    if (noteWindow.ShowDialog() == true)
                    {
                        Note note = noteWindow.Note;
                        _dataBase.Notes.Add(note);
                        _dataBase.SaveChanges();
                    }
                });
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??= new RelayCommand((selectedItem) =>
                {
                    // получаем выделенный объект
                    Note note = selectedItem as Note;

                    if (note == null) return;

                    Note vm = new Note
                    {
                        Id = note.Id,
                        Title = note.Title,
                        Description = note.Description
                    };

                    NoteWindow noteWindow = new NoteWindow(vm);

                    if (noteWindow.ShowDialog() == true)
                    {
                        note.Title = noteWindow.Note.Title;
                        note.Description = noteWindow.Note.Description;
                        _dataBase.Entry(note).State = EntityState.Modified;
                        _dataBase.SaveChanges();
                    }
                });
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand((selectedItem) =>
                {
                    // получаем выделенный объект
                    Note note = selectedItem as Note;

                    if (note == null)
                    {
                        return;
                    }

                    _dataBase.Notes.Remove(note);
                    _dataBase.SaveChanges();
                });
            }
        }

        public RelayCommand DeleteInsideListCommand
        {
            get
            {
                return _deleteCommand;
            }
        }
    }
}