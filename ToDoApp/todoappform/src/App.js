import logo from './logo.svg';
import './App.css';
import {Component} from 'react';

class App extends Component{
  constructor(props){
    super(props);
    this.state={
      notes:[ {id: 1, someattr: "a string", anotherattr: ""},
      {id: 2, someattr: "another string", anotherattr: ""},
      {id: 3, someattr: "a string", anotherattr: ""},]
    }
  }
  API_URL="http://localhost:5134";

  componentDidMount(){
this.refreshNotes();
  }

  async refreshNotes(){
    fetch(this.API_URL+"/api/ToDoApp/GetNotes").
    then(response=> response.json() ).
    then(data=>{this.setState({notes:data})
    
  });
 // console.log("from api"+{notes:this.state});

  }

  async addNotes(){
    let formData=new FormData();
    var newNote=document.getElementById("addNotes").value;
    formData.append("newNote",newNote);
    fetch(this.API_URL+"/api/ToDoApp/AddNotes",{
      method:"POST",
      //headers: {'Content-Type': 'multipart/form-data'},
      body:formData
    }).
    then(response=>response.json() ).
    then((result)=>{
      alert(result);
      this.refreshNotes();
    }).
    catch((error)=>{
      alert(error);
    })
  }

  render() {
    const{notes}=this.state;
    console.log("notes"+notes);
    return(
    <div className="App">
      <h2>To do App</h2>
      <input id="addNotes"/>&nbsp;
      <button onClick={()=>this.addNotes()}>Add Notes</button>
      {notes.map(note=>
        <p>
          {note.Description}&nbsp;
      <button onClick={()=>this.deleteNotes(note.Id)}>Delete Notes</button>
        </p>)}

    </div>
    )
  }
}

export default App;
