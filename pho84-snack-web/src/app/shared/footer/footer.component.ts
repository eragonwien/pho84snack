import { Component, OnInit } from "@angular/core";
import { DataService } from "src/app/services/data.service";
import { Contact, OpenHour } from "src/app/models";

@Component({
  selector: "app-footer",
  templateUrl: "./footer.component.html",
  styleUrls: ["./footer.component.css"]
})
export class FooterComponent implements OnInit {
  contact: Contact;
  openHours: OpenHour[];

  constructor(private ds: DataService) {}

  ngOnInit() {
    this.ds.contact.subscribe((res: Contact) => {
      this.contact = res;
      if (this.contact.phone && !this.contact.phone.startsWith("0")) {
        this.contact.phone = "0" + this.contact.phone;
      }
    });

    this.ds.openHours.subscribe((res: OpenHour[]) => {
      this.openHours = res;
    });
  }
}
