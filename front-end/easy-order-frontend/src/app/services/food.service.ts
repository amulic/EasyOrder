import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IFood} from "../models/IFood";


@Injectable({
  providedIn: 'root'
})
export class FoodService {

  url:string="https://localhost:7282/api/";
  constructor(private httpC:HttpClient) { }

  getAllFoods() :Observable<IFood[]> {
    return this.httpC.get<IFood[]>(`${this.url}Food`);
  }
}
