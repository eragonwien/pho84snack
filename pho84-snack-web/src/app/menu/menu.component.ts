import { Component, OnInit } from "@angular/core";
import { DataService } from "../services/data.service";
import { Menu } from "../models";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.css"]
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
