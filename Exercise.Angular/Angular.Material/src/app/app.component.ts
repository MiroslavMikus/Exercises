import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isChecked=true;

  onChange($event){
    console.log(event);
  }

  color:number;
  
  colors=[
    {id:1 ,name :'Red'},
    {id:2 ,name :'Green'},
    {id:3 ,name :'Blue'}
  ]
  
}
