import { Component, OnInit } from '@angular/core';
import { Satellite, SatellitesService } from '@example/webapi-contract';
import { take } from 'rxjs/operators';

import { AuthService } from '../auth.service';


@Component({
  selector: 'app-satellites',
  templateUrl: './satellites.component.html',
  styleUrls: ['./satellites.component.scss']
})
export class SatellitesComponent implements OnInit {

  satellites: Satellite[] = [];

  constructor(
    private authService: AuthService,
    private satellitesService: SatellitesService
    ) { }

  ngOnInit(): void {
    const result = this.satellitesService.get()

    result.pipe(take(1)).subscribe(x => {
      this.satellites = x;
    }, _ => {
      this.authService.logout();
    })
  }

}
