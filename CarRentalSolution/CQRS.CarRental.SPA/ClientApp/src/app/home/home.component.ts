import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';   

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  public cars: Car[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Car[]>('http://localhost:5001/api/cars').subscribe(result => {
      this.cars = result as Car[];
    }, error => console.error(error));
  }
}

//type GUID = string & { isGuid: true };

//function guid(guid: string): GUID {
//  return guid as GUID;
//}

//declare let c: Car;
//c.id = guid("guid data");
//c.id.split('-');

interface Car{
  carId: string;
  registrationNumber: string;
  xPosition: number;
  yPosition: number;
  status: Status;
}

enum Status {
  wolny, wypożyczony
}
