import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
})

export class CarComponent {
  public cars: Car[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Car[]>(baseUrl + '/cars').subscribe(result => {
      this.cars = result as Car[];
    },error =>console.error(error))
  }
}

interface Car {
  carId: string;
  registrationNumber: string;
  postionX: number;
  positionY: number;
  status: Status; 
} 
enum Status {
  wolny, wypożyczony
}
