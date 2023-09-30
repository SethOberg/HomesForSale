using System;
using HomesForSaleBLL;
using Gtk;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using HomesForSaleBLL.Dictionary;
using HomesForSaleBLL.Commercials;
using HomesForSaleBLL.Residentals;
using UtilitiesLibrary;
using System.Runtime.Serialization;
using UtilitiesLibrary.ConvertString;

public partial class MainWindow : Gtk.Window
{
    private Gtk.ListStore estateStore = new Gtk.ListStore(typeof(int), typeof(string));
    private Gtk.ListStore searchStore = new Gtk.ListStore(typeof(int), typeof(string));
    private EstateManager manager = new EstateManager();
    private CityDictionary cities = new CityDictionary();
    private bool changed = false;
    private string currentFile = "";
    private int oldId;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        addComboBoxTypes();
        initializeList();
        initializeSearchList();
        

        label42.Text = "";
        label2.Text = "";

        button15.Visible = true;
        button13.Visible = true;
        button1.Visible = false;
        button4.Visible = false;
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    //save
    //when save is clicked, either a new property will be
    //added, or a current one will be changed
    protected void OnButton15Clicked(object sender, EventArgs e)
    {
        if (okToSave())
        {
            String type = combobox7.ActiveText;
            int id = StringToNumbers.convertToInt(entry15.Text);
            String country = combobox3.ActiveText;
            String legalForm = combobox5.ActiveText;
            Countries countryEnum = (Countries)Enum.Parse(typeof(Countries), country);
            LegalForm legalFormEnum = (LegalForm)Enum.Parse(typeof(LegalForm), legalForm);
            int zipcode = StringToNumbers.convertToInt(entry9.Text);
            Adress tempAdress = new Adress(entry7.Text, zipcode, entry11.Text, countryEnum);

            if (!manager.idExists(id))
            {
                changed = true;
                
                createNewEstate(type, id, tempAdress, legalFormEnum);
                label42.Text = "New estate created";
            } else
            {
                label42.Text = "Id already used by another estate";
            }
        
        } else
        {
            label42.Text = "Make sure all fields are filled in" +
                "\nMake sure zipcode and estateid are written with numbers"
                + "\nMake sure id is not already used by another estate"
                ;
        }

    }


    public void createNewEstate(string type, int id, Adress tempAdress, LegalForm legalFormEnum)
    {
        
        if (type.Equals("Warehouse"))
        {
            Warehouse temp = new Warehouse(id, tempAdress, legalFormEnum);
            manager.addEstate(temp);
            cities.addToDictionary(temp.EstateAdress.City, temp);
        }
        else if (type.Equals("Shop"))
        {
            Shop temp = new Shop(id, tempAdress, legalFormEnum);
            manager.addEstate(temp);
            cities.addToDictionary(temp.EstateAdress.City, temp);
        }
        else if (type.Equals("Apartment"))
        {
            Apartment temp = new Apartment(id, tempAdress, legalFormEnum);
            manager.addEstate(temp);
            cities.addToDictionary(temp.EstateAdress.City, temp);
        }
        else if (type.Equals("House"))
        {
            House temp = new House(id, tempAdress, legalFormEnum);
            manager.addEstate(temp);
            cities.addToDictionary(temp.EstateAdress.City, temp);
        }
        else if (type.Equals("Townhouse"))
        {
            Townhouse temp = new Townhouse(id, tempAdress, legalFormEnum);
            manager.addEstate(temp);
            cities.addToDictionary(temp.EstateAdress.City, temp);
        }
        else if (type.Equals("Villa"))
        {
            Villa temp = new Villa(id, tempAdress, legalFormEnum);
            manager.addEstate(temp);
            cities.addToDictionary(temp.EstateAdress.City, temp);
        }

        addNewEstateToList(id, type);
        clearFields();
    }


    //If there is already an Estate with the chosen index
    //Remove the old estate and create a ne one
    public void removeIdFromGuiList(int removeId)
    {
        TreeModel treeModel = treeview5.Model;
        TreeIter iter;
        int parsedId;
        string temp;

        //check value of first entry in list
        treeview5.Model.GetIterFirst(out iter);
        temp = treeModel.GetValue(iter, 0).ToString();
        parsedId = StringToNumbers.convertToInt(temp);
        Console.WriteLine("Parsed id: " + parsedId);
        if (parsedId == removeId)
        {
            estateStore.Remove(ref iter);
        }

        //iterate through the rest of the list
        while (treeModel.IterNext(ref iter))
        {
            //parsedId = Int32.Parse((string)treeModel.GetValue(iter, 0));
            temp = treeModel.GetValue(iter, 0).ToString();
            parsedId = StringToNumbers.convertToInt(temp);
            Console.WriteLine("Parsed id: " + parsedId);
            if (parsedId == removeId)
            {
                estateStore.Remove(ref iter);
                break;
            }
        }
    }



    //checks if all fields are correctly filled
    //if one or more fields are incorrectly filled
    //the user cannot add or change an estate object
    public bool okToSave()
    {
        bool temp = true;

        if(entry7.Text.Equals(""))
        {
            return false;
        }
        if(entry11.Text.Equals("")) {
            return false;
        }
        if(StringToNumbers.stringToIntOk(entry9.Text))
        {
        } else
        {
            return false;
        }
        if(StringToNumbers.stringToIntOk(entry15.Text)) {
        }else
        {
            return false;
        }

        //Ta bort denna?
        //Lägg istället till som if sats i save funktionen?
        //if(manager.idExists(res2))
        //{
        //    return false;
        //}

        return temp;
    }

    //clear
    //empties all filled in fields
    protected void OnButton13Clicked(object sender, EventArgs e)
    {
        clearFields();
    }

    //delete
    //deletes a selected object from the tree/list view of estates
    protected void OnButton17Clicked(object sender, EventArgs e)
    {
        TreeSelection selection = treeview5.Selection;
        TreeIter iter;
        TreeModel model;

        if (((TreeSelection)selection).GetSelected(out model, out iter))
        {
            int id = (int)model.GetValue(iter, 0);
            string type = (string)model.GetValue(iter, 1);
            int index = StringToNumbers.convertToInt(model.GetStringFromIter(iter));


            cities.removeFromDictionary(manager.getEstate(index).EstateAdress.City, id);
            manager.removeAt(index);
            estateStore.Remove(ref iter);

            //clear textview
            textview5.Buffer.Text = "";
        }

    }

    //change estate object
    //loads all the information about the estate
    protected void OnButton18Clicked(object sender, EventArgs e)
    {
        notebook1.Page = 0;
        button15.Visible = false;
        button13.Visible = false;
        button1.Visible = true;
        button4.Visible = true;

        TreeSelection selection = treeview5.Selection;
        TreeIter iter;
        TreeModel model;

        if (((TreeSelection)selection).GetSelected(out model, out iter))
        {
            int id = (int)model.GetValue(iter, 0);
            string type = (string)model.GetValue(iter, 1);

            Estate temp = manager.getEstate(StringToNumbers.convertToInt(model.GetStringFromIter(iter)));

            if (temp == null)
            {
                label42.Text = "Null Estate";
            }
            else
            {
                //Keep track of estate user wants to change
                oldId = temp.EstateID;

                entry15.Text = temp.EstateID.ToString();
                entry7.Text = temp.EstateAdress.Street;
                entry9.Text = temp.EstateAdress.ZipCode.ToString();
                entry11.Text = temp.EstateAdress.City;
                showEstateEnums(temp);
                label42.Text = "Click save to change estate";
            }
            
        }

    }


    //Show legalform, estatetype and country for existing estate
    public void showEstateEnums(Estate estate)
    {
        TreeModel model = combobox5.Model;
        TreeIter iter;
        
        String temp;
        String legalForm = estate.LegalForm.ToString();
        String country = estate.EstateAdress.Country.ToString();
        String estatetype = estate.GetType().Name;

        combobox5.Model.GetIterFirst(out iter);

        temp = (string)model.GetValue(iter, 0);
        if(temp.Equals(legalForm))
        {
            combobox5.SetActiveIter(iter);
        }
        while (model.IterNext(ref iter))
        {
            temp = (string)model.GetValue(iter, 0);
            if(temp.Equals(legalForm))
            {
                combobox5.SetActiveIter(iter);
                break;
            }
        }

        model = combobox3.Model;
        combobox3.Model.GetIterFirst(out iter);

        temp = (string)model.GetValue(iter, 0);
        if (temp.Equals(country))
        {
            combobox3.SetActiveIter(iter);
        }
        while (model.IterNext(ref iter))
        {
            temp = (string)model.GetValue(iter, 0);
            if (temp.Equals(country))
            {
                combobox3.SetActiveIter(iter);
                break;
            }
        }

        model = combobox7.Model;
        combobox7.Model.GetIterFirst(out iter);

        temp = (string)model.GetValue(iter, 0);
        if (temp.Equals(estatetype))
        {
            combobox7.SetActiveIter(iter);
        }

        while (model.IterNext(ref iter))
        {
            temp = (string)model.GetValue(iter, 0);
            if (temp.Equals(estatetype))
            {
                combobox7.SetActiveIter(iter);
                break;
            }
        }

    }

    //view
    protected void OnButton19Clicked(object sender, EventArgs e)
    {
        notebook1.Page = 1;
        textview5.Buffer.Text = "";

        TreeSelection selection = treeview5.Selection;
        TreeIter iter;
        TreeModel model;
        String description = "";

        if (((TreeSelection)selection).GetSelected(out model, out iter))
        {
            int id = (int)model.GetValue(iter, 0);
            string type = (string)model.GetValue(iter, 1);
            int index = StringToNumbers.convertToInt(model.GetStringFromIter(iter));
            description = manager.getEstate(index).toString();
            textview5.Buffer.Text = description;
        }

        
    }

    protected void clearFields()
    {
        entry7.Text = "";
        entry9.Text = "";
        entry11.Text = "";
        entry15.Text = "";

        label42.Text = "";
        label2.Text = "";
    }

    public void clearList()
    {
        manager = new EstateManager();
        estateStore.Clear();
    }

    public void clearSession()
    {
        clearFields();
        clearList();
        clearSearchResults();
    }

    public void addComboBoxTypes()
    {
        string[] temp = (string[])Enum.GetNames(typeof(Countries));
        for(int i = 0; i < temp.Length; i++)
        {
            combobox3.AppendText(temp[i]);
        }

        temp = (string[])Enum.GetNames(typeof(LegalForm));

        for (int i = 0; i < temp.Length; i++)
        {
            combobox5.AppendText(temp[i]);
        }

        temp = (string[])Enum.GetNames(typeof(EstateType));

        for (int i = 0; i < temp.Length; i++)
        {
            combobox7.AppendText(temp[i]);
        }

        combobox3.Active = 0;
        combobox5.Active = 0;
        combobox7.Active = 0;
    }


    public void addObjectsToList()
    {
        clearList();

    }

    public void saveToFile(string filename)
    {
        //Spara List<T> objektet istället för hela manager objektet???
        //Testa imorgon
        //Läs mer om interfacet ISerializeable!
        string extension = System.IO.Path.GetExtension(filename);

        if(extension.Equals(".xml"))
        {
            manager.XMLSerialize(filename);
        } else
        {
            //binarySerializer.saveFile(filename, manager);
            manager.BinarySerialize(filename);
        }

        
    }

    public void getSavedFile(string filename)
    {
        //Check if file extension is .xml or txt or dat
        string extension = System.IO.Path.GetExtension(filename);

        if(extension.Equals(".xml"))
        {
            manager.XMLDeSerialize(filename);
        } else
        {
            manager.BinaryDeSerialize(filename);
            
        }

        cities.initializeDictionary(manager.getEstates());

        Estate temp = null;

        if(manager != null)
        {
            for (int i = 0; i < manager.Count; i++)
            {
                temp = manager.getEstate(i);
                addNewEstateToList(temp.EstateID, temp.getEstateType());
            }
        } else
        {
            Debug.WriteLine("Manager objeket null");
            manager = new EstateManager();
        }

        

    }

    public void initializeList()
    {
        Gtk.TreeViewColumn IdColumn = new Gtk.TreeViewColumn();
        IdColumn.Title = "EstateId";
        Gtk.TreeViewColumn TypeColumn = new Gtk.TreeViewColumn();
        TypeColumn.Title = "EstateType";

        treeview5.AppendColumn(IdColumn);
        treeview5.AppendColumn(TypeColumn);

        Gtk.CellRendererText estateIdCell = new Gtk.CellRendererText();
        IdColumn.PackStart(estateIdCell, true);

        Gtk.CellRendererText estateTypeCell = new Gtk.CellRendererText();
        TypeColumn.PackStart(estateTypeCell, true);

        IdColumn.AddAttribute(estateIdCell, "text", 0);
        TypeColumn.AddAttribute(estateTypeCell, "text", 1);

        treeview5.Model = estateStore;

    }

    public void addNewEstateToList(int id, string type)
    {
        estateStore.AppendValues(id, type);
    }

    protected void OnTreeview5RowActivated(object o, RowActivatedArgs args)
    {
    }


    //Event handlers for all the menu items

    protected void OnCloseActionActivated(object sender, EventArgs e)
    {
        //warn users about saving before closing app
        Gtk.MessageDialog messageDialog = new Gtk.MessageDialog(
            this, DialogFlags.DestroyWithParent, MessageType.Question
            , ButtonsType.YesNo, "Are you sure you want to exit program?");

        ResponseType response = (ResponseType)messageDialog.Run();

        if (response == ResponseType.Yes)
        {
            Application.Quit();
        }
        else
        {

        }

        messageDialog.Destroy();

    }

    public bool listEmpty()
    {
        if(manager.Count == 0)
        {
            return true;
        }

        return false;
    }


    //allows user to open an XML file
    protected void OnImportFromXMLFileActionActivated(object sender, EventArgs e)
    {
        Gtk.FileChooserDialog fc =
        new Gtk.FileChooserDialog("Choose the file to open",
                                    this,
                                    FileChooserAction.Open,
                                    Gtk.Stock.Cancel, ResponseType.Cancel,
                                    Gtk.Stock.Open, ResponseType.Accept);
        FileFilter filter = new FileFilter();
        filter.Name = "XML files";
        filter.AddMimeType("file/xml");
        filter.AddPattern("*.xml");
        fc.AddFilter(filter);
        if (fc.Run() == (int)ResponseType.Accept)
        {
            clearSession();
            currentFile = fc.Filename;
            getSavedFile(currentFile);
        }

        fc.Destroy();
    }


    //Checks if the current file is an xml file, if so the current list will be saved to that file
    //Otherwise the user has to create and save a new file
    protected void OnExportToXMLFileActionActivated(object sender, EventArgs e)
    {
        changed = false;

        if (!currentFile.Equals(""))
        {
            if (System.IO.Path.GetExtension(currentFile).Equals(".xml"))
                {
                    label2.Text = "File saved";
                    saveToFile(currentFile);
                    changed = false;
                }
            else
            {
                label2.Text = "Cannot export to xml, wrong file type";
            }

        } else
        {
            //save as funktion
            if(!listEmpty())
            {
                Gtk.FileChooserDialog fc =
            new Gtk.FileChooserDialog("Write a filename",
                                        this,
                                        FileChooserAction.Save,
                                        "Cancel", ResponseType.Cancel,
                                        "Save", ResponseType.Accept);

                FileFilter filter = new FileFilter();
                filter.Name = "XML";
                filter.AddMimeType(".xml");
                filter.AddPattern(".*xml");
                fc.AddFilter(filter);

                if (fc.Run() == (int)ResponseType.Accept)
                {


                    if (System.IO.Path.HasExtension(fc.Filename))
                    {
                        if (System.IO.Path.GetExtension(fc.Filename).Equals(".xml"))
                        {
                            label2.Text = "File saved";
                            //kalla på läs funktion här
                            saveToFile(fc.Filename);
                            changed = false;
                        }
                        else
                        {
                            label2.Text = "File extension not supported";
                        }
                    }
                    else
                    {
                        label2.Text = "Add a file extension";
                    }



                }
                fc.Destroy();
            }
            else
            {
                label2.Text = "List empty, cannot save";
            }
        }

    }


    //Lets user choose a filename and fileextension to save a new file
    protected void OnSaveAsActionActivated(object sender, EventArgs e)
    {
        

        if(!listEmpty())
        {
            Gtk.FileChooserDialog fc =
            new Gtk.FileChooserDialog("Write a filename",
                                        this,
                                        FileChooserAction.Save,
                                        "Cancel", ResponseType.Cancel,
                                        "Save", ResponseType.Accept);

            FileFilter filter = new FileFilter();
            filter.Name = "Dat";
            filter.AddMimeType(".dat");
            filter.AddPattern(".*dat");
            fc.AddFilter(filter);
            filter.Name = "Txt";
            filter.AddMimeType(".txt");
            filter.AddPattern(".*txt");
            fc.AddFilter(filter);

            if (fc.Run() == (int)ResponseType.Accept)
            {


                if (System.IO.Path.HasExtension(fc.Filename))
                {
                    if (System.IO.Path.GetExtension(fc.Filename).Equals(".dat") || System.IO.Path.GetExtension(fc.Filename).Equals(".txt"))
                    {
                        label2.Text = "File saved";
                        //kalla på läs funktion här
                        saveToFile(fc.Filename);
                        changed = false;
                    }
                    else
                    {
                        label2.Text = "File extension not supported";
                    }
                }
                else
                {
                    label2.Text = "Add a file extension";
                }



            }
            fc.Destroy();
        } else
        {
            label2.Text = "List empty, cannot save";
        }
        
    }

    //If there is a current saved filename from opening a file, the save option will save to that file
    //If there is no saved filename the save as function will be called
    protected void OnSaveActionActivated(object sender, EventArgs e)
    {
        changed = false;

        if(currentFile.Equals(""))
        {
            OnSaveAsActionActivated(sender, e);
        } else
        {
            saveToFile(currentFile);
        }

    }

    //Open file dialog to let user choose file
    protected void OnOpenActionActivated(object sender, EventArgs e)
    {
        Gtk.FileChooserDialog fc =
        new Gtk.FileChooserDialog("Choose the file to open",
                                    this,
                                    FileChooserAction.Open,
                                    Gtk.Stock.Cancel, ResponseType.Cancel,
                                    Gtk.Stock.Open, ResponseType.Accept);
        FileFilter filter = new FileFilter();
        filter.Name = "Txt and Dat files";
        filter.AddMimeType("file/txt");
        filter.AddPattern("*.txt");
        filter.AddMimeType("file/dat");
        filter.AddPattern("*.dat");
        fc.AddFilter(filter);
        if (fc.Run() == (int)ResponseType.Accept)
        {
            clearSession();
            currentFile = fc.Filename;
            getSavedFile(currentFile);
        }
        fc.Destroy();
    }

    protected void OnNewActionActivated(object sender, EventArgs e)
    {
        //warn users about saving before clearing current session
        if(changed)
        {
            Gtk.MessageDialog messageDialog = new Gtk.MessageDialog(
            this, DialogFlags.DestroyWithParent, MessageType.Question
            , ButtonsType.YesNo, "Are you sure you want to continue without saving?");

            ResponseType response = (ResponseType)messageDialog.Run();

            if (response == ResponseType.Yes)
            {
                clearSession();
            }

            messageDialog.Destroy();
        } else
        {
            clearSession();
        }
        

    }


    //search button
    protected void OnButton3Clicked(object sender, EventArgs e)
    {
        
        if(entry3.Text.Equals(""))
        {
            label4.Text = "Field empty, cannot search";
        } else
        {
            clearSearchResults();
            showSearchResults();
        }

    }


    //The gui list showing the search results
    public void initializeSearchList()
    {
        Gtk.TreeViewColumn IdColumn = new Gtk.TreeViewColumn();
        IdColumn.Title = "EstateId";
        //Gtk.TreeViewColumn TypeColumn = new Gtk.TreeViewColumn();
        //TypeColumn.Title = "EstateType";
        treeview3.AppendColumn(IdColumn);
        //treeview3.AppendColumn(TypeColumn);
        Gtk.CellRendererText estateIdCell = new Gtk.CellRendererText();
        IdColumn.PackStart(estateIdCell, true);
        //Gtk.CellRendererText estateTypeCell = new Gtk.CellRendererText();
        //TypeColumn.PackStart(estateTypeCell, true);
        IdColumn.AddAttribute(estateIdCell, "text", 0);
        //TypeColumn.AddAttribute(estateTypeCell, "text", 1);

        treeview3.Model = searchStore;
    }


    //Gets the text, city put into the search field and checks if there is a match in the dictionary
    //If there is a match, the list of estates in the city will be displayed
    public void showSearchResults()
    {
        //get text from search input field
        string res = entry3.Text;

        List<int> estates = cities.EstatesFromCity(res);

        if(cities.cityExixtsInDictionary(res))
        {
            for(int i = 0; i < estates.Count; i++)
            {
                //addNewEstateSearchResult(estates[i], estates[i].LegalForm.ToString());
                addNewEstateSearchResult(estates[i]);
            }

        } else
        {
            label4.Text = "No results found";
        }

    }

    public void clearSearchResults()
    {
        searchStore.Clear();
    }

    //public void addNewEstateSearchResult(int id, string type)
    //{
    //    searchStore.AppendValues(id, type);
    //}

    public void addNewEstateSearchResult(int id)
    {
        searchStore.AppendValues(id);
    }

    //accept change button
    //user is not allowed to change the id of an estate
    protected void OnButton1Clicked(object sender, EventArgs e)
    {
        if (okToSave() && manager.idExists(oldId))
        {
            int newId = StringToNumbers.convertToInt(entry15.Text);
            String type = combobox7.ActiveText;
            String country = combobox3.ActiveText;
            String legalForm = combobox5.ActiveText;
            Countries countryEnum = (Countries)Enum.Parse(typeof(Countries), country);
            LegalForm legalFormEnum = (LegalForm)Enum.Parse(typeof(LegalForm), legalForm);
            int zipcode = StringToNumbers.convertToInt(entry9.Text);

            if (oldId == newId || !manager.idExists(newId))
            {
                Adress tempAdress = new Adress(entry7.Text, zipcode, entry11.Text, countryEnum);

                changed = true;
                //when change ok
                int indexOfExistingEstate = manager.getIndexOfEstate(oldId);
                removeIdFromGuiList(oldId);
                cities.removeFromDictionary(manager.getEstate(indexOfExistingEstate).EstateAdress.City, oldId);
                manager.removeAt(indexOfExistingEstate);
                createNewEstate(type, newId, tempAdress, legalFormEnum);

                //Once estate changed, hide change button and show add button
                button15.Visible = true;
                button13.Visible = true;
                button1.Visible = false;
                button4.Visible = false;
                label42.Text = "Estate changed";
            } else
            {
                label42.Text = "Id already used by another estate";
            }

        } else
        {
            label42.Text = "Make sure all fields are filled in" +
                "\nMake sure zipcode and estateid are written with numbers"
                ;
        }

    }


    //cancel change
    protected void OnButton4Clicked(object sender, EventArgs e)
    {
        button15.Visible = true;
        button13.Visible = true;
        button1.Visible = false;
        button4.Visible = false;

        clearFields();
    }
}
