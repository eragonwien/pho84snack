import { Component, OnInit } from "@angular/core";
import { DataService } from "../services/data.service";
import { Menu } from "../models";
import { fadeInOut } from "../shared/animations";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.css"],
  animations: [fadeInOut]
})
export class MenuComponent implements OnInit {
  menus: Menu[];
  constructor(private ds: DataService) {}

  ngOnInit() {
    this.ds.menu.subscribe((menus: Menu[]) => {
      this.menus = menus;
    });
  }
}
