import { Component, OnInit } from '@angular/core';
import {DataService} from "../data.service";
import {HttpClient} from "@angular/common/http";
import { CountryGetallResponse, CountryGetallResponseObj } from "./country-getall-response";
import { MojConfig } from "../my-config"

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
 data:any;


    constructor(public httpClient: HttpClient ) {
    }


    drzave: CountryGetallResponseObj[] = [];
    ngOnInit(): void {
        let url = MojConfig.adresa_servera + `/api/Country`
        this.httpClient.get<CountryGetallResponse>(url).subscribe((x: CountryGetallResponse) => {
            this.drzave = x.countries;
        })
    }
    preuzmiNovePodatke($event: Event) {
        // @ts-ignore
        let naziv = $event.target.value;
        let url = MojConfig.adresa_servera + `/api/Country`
        this.httpClient.get<CountryGetallResponse>(url).subscribe((x: CountryGetallResponse) => {
            this.drzave = x.countries;
        })
    }
}
