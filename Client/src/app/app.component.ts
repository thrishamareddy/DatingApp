import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component,inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
// import { platformBrowser } from '@angular/platform-browser';
// import { ApplicationModule } from '@angular/core';
 
// platformBrowser().bootstrapModule(ApplicationModule).catch((err) => console.error(err));
@Component({
  selector: 'app-root',
  standalone: true,
      imports: [AppComponent,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http=inject(HttpClient);
  // constructor(private http:HttpClient) { }
  ngOnInit(){
    this.getUsers();
  }
  title = 'Dating App';
  users:any;
  getUsers(){
    this.http.get('http://localhost:5264/api/users').subscribe(res=>{
      this.users=res;
    },error=>{
      console.log(error);
    })
  }
}
