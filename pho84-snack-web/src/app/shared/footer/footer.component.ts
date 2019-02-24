import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { Restaurant, Contact, OpenHour } from 'src/app/models';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  restaurant: Restaurant;
  contact: Contact;
  openHours: OpenHour[];

  constructor(private ds: DataService) { }

  ngOnInit() {
    this.ds.restaurant.subscribe((res: Restaurant) => {
      this.restaurant = res;
    });

    this.ds.contact.subscribe((res: Contact) => {
      this.contact = res;
    });

    this.ds.openHours.subscribe((res: OpenHour[]) => {
      this.openHours = res;
    });
  }
}
