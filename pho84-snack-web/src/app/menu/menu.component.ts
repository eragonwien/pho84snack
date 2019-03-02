import { Component, OnInit } from "@angular/core";
import { DataService } from "../services/data.service";
import { Category } from "../models";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.css"]
})
export class MenuComponent implements OnInit {
  categories: Category[];
  constructor(private ds: DataService) {}

  ngOnInit() {
    this.ds.category.subscribe((categories: Category[]) => {
      this.categories = categories.filter(c => c.type === "menu");
    });
  }
}
