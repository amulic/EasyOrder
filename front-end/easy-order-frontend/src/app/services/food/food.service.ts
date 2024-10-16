import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IFood} from "../../models/IFood";


@Injectable({
  providedIn: 'root'
})
export class FoodService {

  url:string="https://localhost:7282/api";
  constructor(private httpC:HttpClient) { }

  getAllFoods() :Observable<IFood[]> {
    return this.httpC.get<IFood[]>(`${this.url}/Food`, {withCredentials:true});
  }

  createFood(data:FormData) :Observable<IFood> {
    const food: Partial<IFood> = {
      name: data.get('name') as string,
      price: Number.parseFloat(data.get('price') as string),
      description: data.get('description') as string,
      imageLink: data.get('imageLink') as string
    }
    console.log("data", data);
    console.log("food", food);
    return this.httpC.post<IFood>(`${this.url}/Food`, food, {withCredentials:true});
  }
}
