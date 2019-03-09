import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Contact, Category, Feature, OpenHour, Menu } from "../models";

@Injectable()
export class DataService {
  constructor(private http: HttpClient) {}

  get contact() {
    return this.http.get<Contact>("/assets/contact.json");
  }
  get openHours() {
    return this.http.get<OpenHour[]>("/assets/open.hour.json");
  }
  get category() {
    return this.http.get<Category[]>("/assets/category.json");
  }
  get features() {
    return this.http.get<Feature[]>("/assets/features.json");
  }
  get gallery() {
    return this.http.get<string[]>("/assets/gallery.json");
  }
  get menu() {
    return this.http.get<Menu[]>("/assets/menu.json");
  }
}
