import { Component, OnInit } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { collapseAnimation } from "../animations";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.css"],
  animations: [collapseAnimation]
})
export class NavbarComponent implements OnInit {
  openBurger: boolean = false;

  constructor(private router: Router) {}

  ngOnInit() {
    this.subscribeRouteChange();
  }

  toggleBurger() {
    this.openBurger = !this.openBurger;
  }

  closeBurger() {
    this.openBurger = false;
  }

  subscribeRouteChange() {
    this.router.events.subscribe((res: NavigationEnd) => {
      this.handleRouteChange();
    });
  }

  handleRouteChange() {
    this.closeBurger();
  }
}
