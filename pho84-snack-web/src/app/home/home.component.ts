import { Component, OnInit } from '@angular/core';
import { Restaurant, Contact, Category } from '../models';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  restaurant: Restaurant;
  contact: Contact;
  categories: Category[];

  constructor(private ds: DataService) { }

  ngOnInit() {
    this.ds.restaurant.subscribe((res: Restaurant) => {
      this.restaurant = res;
    });

    this.ds.contact.subscribe((res: Contact) => {
      this.contact = res;
    });

    this.ds.category.subscribe((res: Category[]) => {
      this.categories = res;
    });
  }

}
