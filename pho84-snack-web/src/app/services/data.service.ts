import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Restaurant, Contact, Category, Feature } from '../models';

@Injectable()
export class DataService {

  constructor(private http: HttpClient) { }

  get restaurant() { return this.http.get<Restaurant>("assets/restaurant.json"); }
  get contact() { return this.http.get<Contact>("assets/contact.json"); }
  get category() { return this.http.get<Category[]>("assets/category.json"); }
  get features() { return this.http.get<Feature[]>("assets/features.json"); }
  get gallery() { return this.http.get<string[]>("assets/gallery.json"); }
}
